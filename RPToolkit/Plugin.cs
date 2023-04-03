using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using System.Reflection;
using Dalamud.Interface.Windowing;
using RPToolkit.Windows;
using System;
using Dalamud.Game;
using Dalamud.Logging;
using System.Collections.Generic;
using Dalamud;
using Lumina.Excel.GeneratedSheets;
using Dalamud.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.Gui;
using Dalamud.Interface;
using Dalamud.Game.ClientState;
using ImGuiNET;
using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System.Security.Cryptography;
using Dalamud.Hooking;
using Dalamud.Utility.Signatures;
using Dalamud.Memory;
using FFXIVClientStructs.FFXIV.Client.Graphics;
using FFXIVClientStructs.STD;
using CharacterPanelRefined;
using static CharacterPanelRefined.Tooltips;
using FFXIVClientStructs.Interop;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using Dalamud.Plugin.Internal;
using Lumina;
using System.Reflection.Metadata;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin.Ipc;
using Dalamud.Game.ClientState.Resolvers;
using Dalamud.Interface.Internal.Notifications;
using Newtonsoft.Json.Linq;
using FFXIVClientStructs.FFXIV.Client.UI;
using RPToolkit.Handlers;
using RPToolkit.Helpers;

namespace RPToolkit
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {
        public static Plugin Singleton { get; private set; }

        public System.Drawing.Rectangle gameDimensions { get; private set; } = new System.Drawing.Rectangle();
        [PluginService] static internal SigScanner SigScanner { get; private set; }
        [PluginService] static internal DataManager Data { get; private set; }
        [PluginService] static internal Framework Framework { get; private set; }
        [PluginService] static internal Dalamud.Game.ClientState.Conditions.Condition Condition { get; private set; }
        public static GameGui GameGui { get; private set; } = null!;
        public ClientState clientState { get; private set; }
        public static ChatGui chat { get; private set; } = null!;
        public static UiBuilder UiBuilder { get; private set; } = null!;

        private readonly ObjectTable _objectTable;
        private readonly ICallGateSubscriber<int> _glamourerApiVersion;
        private readonly ICallGateSubscriber<string, GameObject?, object>? _glamourerApplyOnlyEquipment;
        private readonly ICallGateSubscriber<GameObject?, string>? _glamourerGetAllCustomization;
        private string currentCustomizationString;
        public bool glamourerAvailable { get; private set; }

        private bool updateCharacterPanel = false;

        public static Dictionary<int, string> parasols { get; private set; } = new Dictionary<int, string>{
            {58001, "Parasol"},
            {58002, "Sky Blue Parasol"},
            {58003, "Vermilion Paper Parasol"},
            {58004, "Plum Paper Parasol"},
            {58005, "Gold Paper Parasol"},
            {58006, "Calming Checkered Parasol"},
            {58007, "Cheerful Checkered Parasol"},
            {58008, "Classy Checkered Parasol"},
            {58009, "Pleasant Dot Parasol"},
            {58010, "Prim Dot Parasol"},
            {58011, "Pastoral Dot Parasol"},
            {58016, "Red Moon Parasol"},
            {58023, "White Lace Parasol"},
            {58024, "Blue Blossom Parasol"},
            {58027, "Sabotender Parasol"},
        };

        System.Timers.Timer tick = new System.Timers.Timer(1000);


        public string Name => "Immersive Flavor Text";

        public DalamudPluginInterface PluginInterface { get; private set; }
        public Configuration _configuration { get; init; }
        public static Configuration Configuration { get { return Plugin.Singleton._configuration; } private set { } }
        public static WindowSystem WindowSystem = new("RPToolkit");

        // I frickin suck at remembering to write comments on personal projects
        // so to anyone reading through this, good luck soldier and god speed
        public Plugin(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        GameGui gameGui,
        ClientState clientState,
        ChatGui chatGui,
        ObjectTable objectTable)
        {
            if (Singleton != null)
            {
                Singleton.Dispose();
            }
            Singleton = this;

            #region Dalamud Initialization
            AppDomain.CurrentDomain.FirstChanceException += HandleException;
            this.PluginInterface = pluginInterface;
            GameGui = gameGui;
            this.clientState = clientState;
            UiBuilder = this.PluginInterface.UiBuilder;
            chat = chatGui;
            _objectTable = objectTable;

            this._configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this._configuration.Initialize(this.PluginInterface);
            #endregion

            #region Internal Initialization
            TimeHelper.Initialize(pluginInterface);
            CommandHandler.Initialize(pluginInterface);
            WeatherHandler.Initialize(pluginInterface);
            #endregion

            #region Other Plugin API Initialization
            _glamourerApiVersion = pluginInterface.GetIpcSubscriber<int>("Glamourer.ApiVersion");
            _glamourerApplyOnlyEquipment = pluginInterface.GetIpcSubscriber<string, GameObject?, object>("Glamourer.ApplyOnlyEquipmentToCharacter");
            _glamourerGetAllCustomization = pluginInterface.GetIpcSubscriber<GameObject?, string>("Glamourer.GetAllCustomizationFromCharacter");
            #endregion

            GetGameDimensions();

            var world = clientState.LocalPlayer?.CurrentWorld.GameData.Name.RawString;
            PluginLog.Information($"Hello {world}!");

            #region Windows Initialization
            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new TempSuggestionWindow(this));
            #endregion

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            ChatHelper.Initialize();

            InitializePeriodicTicks();

            /*bool glamourerInstalled = pluginInterface.PluginInternalNames.Contains("Glamourer");
            PluginLog.Information(glamourerInstalled ? "Glamourer has been detected. Enabling functionality." : "Glamourer not detected.");
            //_objectTable.CreateObjectReference(this.clientState.LocalPlayer?.Address);
            if (this.clientState.LocalPlayer is Character c)
            {
                PluginLog.Information(_glamourerGetAllCustomization!.InvokeFunc(c));
                _glamourerApplyOnlyEquipment!.InvokeAction("Ah3/DwQBAWQHAWaAa3IsWQBVAXKCBAACB0sDKIAsWQIvAAIAALoCMAABAACtEwgAjQABZjwBAWYxAAZmuBcBZmQAAWZiAAEANQABADsAAgA1AAEAAQAAAIA/Aw==", c);
            }
            PluginLog.Information(this.clientState.LocalPlayer?.ClassJob.GameData.Abbreviation);*/

            //var wetness = (float*)(*(IntPtr*)SigScanner.GetStaticAddressFromSig("48 8B 05 ?? ?? ?? ?? 48 83 C1 10 48 89 74 24"));
            //var wetness = SigScanner.ScanData("218895E9D80");
            //PluginLog.Information($"Wetness: {wetness}");
            //PluginLog.Information("Map Title: " + Data.GetExcelSheet<TerritoryType>()?.GetRow(this.clientState.TerritoryType).PlaceName.Value.Name.RawString);
            //this.clientState.TerritoryChanged +=
            //NetHelper.SubmitDataAsync(this.clientState.LocalPlayer?.Name.ToString(), Data.GetExcelSheet<TerritoryType>()?.GetRow(this.clientState.TerritoryType).PlaceName.Value.Name.RawString, currentTemp.ToString(), currentTemp.ToString());



            SignatureHelper.Initialise(this);
            if (updateCharacterPanel)
            {
                tooltips = new Tooltips();
                characterStatusOnSetup.Enable();
            }
        }

        private void InitializePeriodicTicks()
        {
            //would use .net 6 PeriodicTimer but it can't be used in unsafe contexts, research for later
            tick.AutoReset = true;
            tick.Elapsed += WeatherHandler.CheckWeather;
            tick.Elapsed += TemperatureHandler.UpdateTemps;
            tick.Elapsed += GetGameDimensions;
            tick.Elapsed += PeriodicApiStateCheck;
            tick.Elapsed += PeriodicOutfitChangeCheck;
            tick.Start();
        }

        public class Command
        {
            public string command;
            public string description;

            public Command(string commandName, string descriptionText)
            {
                command = commandName;
                description = descriptionText;
            }
        }

        private void HandleException(object? sender, FirstChanceExceptionEventArgs eventArgs)
        {
            if (eventArgs.Exception.Source == GetType().Namespace)
            {
                PluginLog.LogError($"{eventArgs.Exception.Source}: Uh oh, stuff just crashed! Better clean up after myself...");
                PluginLog.LogError(eventArgs.Exception.ToString());
                this.Dispose();
                //Unload Plugin? idk how to do that yet
            }
        }

        public void Dispose()
        {
            tick.Dispose();
            WindowSystem.RemoveAllWindows();
            CommandHandler.Dispose();

            if (updateCharacterPanel)
            {
                characterStatusOnSetup.Dispose();
                tooltips.Dispose();
            }

            AppDomain.CurrentDomain.FirstChanceException -= HandleException;
        }

        private void DrawUI()
        {
            WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            WindowSystem.GetWindow($"{this.Name} Config Window").IsOpen = true;
        }

        public static void DrawWindow(string windowName)
        {
            WindowSystem.GetWindow(windowName).IsOpen = true;
        }

        private void GetGameDimensions()
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool GetWindowRect(IntPtr hWnd, out System.Drawing.Rectangle lpRect);

            // temporary variable since you cannot output to this.gameDimensions
            System.Drawing.Rectangle d;
            GetWindowRect(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, out d);
            this.gameDimensions = d;
        }

        private void GetGameDimensions(Object? source, System.Timers.ElapsedEventArgs e)
        {
            GetGameDimensions();
        }
        private void PeriodicApiStateCheck(Object? source, System.Timers.ElapsedEventArgs e)
        {
            glamourerAvailable = CheckGlamourerApi();
        }
        private bool CheckGlamourerApi()
        {
            bool apiAvailable = false;
            try
            {
                apiAvailable = _glamourerApiVersion.InvokeFunc() >= 0;
                return apiAvailable;
            }
            catch
            {
                return apiAvailable;
            }
        }

        public string GetGlamourerCurrentEquipment()
        {
            if (glamourerAvailable && clientState.LocalPlayer is Character c)
                return _glamourerGetAllCustomization!.InvokeFunc(c);
            else return "";
        }

        public void ApplyGlamourerEquipment(string customizationString)
        {
            if (glamourerAvailable && clientState.LocalPlayer is Character c)
                _glamourerApplyOnlyEquipment!.InvokeAction(customizationString, c);
        }

        private void PeriodicOutfitChangeCheck(Object? source, System.Timers.ElapsedEventArgs e)
        {
            if (glamourerAvailable)
            {
                foreach (var outfitData in Configuration.climateOutfitData)
                {
                    if (clientState.LocalPlayer?.ClassJob.Id == outfitData.jobID)
                    {
                        bool correctWeather = false;
                        if ((outfitData.climateConditions & (int)ClimateOutfitData.ClimateConditions.Only_When_Raining) == (int)ClimateOutfitData.ClimateConditions.Only_When_Raining)
                        {
                            if (WeatherHandler.rainWeathers.Contains(*WeatherHandler.currentWeather))
                            {
                                correctWeather = true;
                            }
                        }
                        else
                            correctWeather = true;

                        if (correctWeather)
                        {
                            //PluginLog.Information(currentTemperatureStage.ToString());
                            //PluginLog.Information(Climates.temperatureStages.ElementAt(0).Key);
                            for (int i = 1; i < Enum.GetValues(typeof(ClimateOutfitData.ClimateConditions)).Length; i++)
                            {
                                int value = ((int[])Enum.GetValues(typeof(ClimateOutfitData.ClimateConditions)))[i];
                                if ((outfitData.climateConditions & value) == value && TemperatureHandler.currentTemperatureStage == Climates.temperatureStages.ElementAt((Climates.temperatureStages.Count - 1) - (i - 1)).Key)
                                {
                                    if (PluginInterface.IsDev)
                                        PluginLog.Information(i + ": " + Enum.GetName(typeof(ClimateOutfitData.ClimateConditions), value) + " (" + Climates.temperatureStages.ElementAt((Climates.temperatureStages.Count - 1) - (i - 1)).Key + ")");
                                    if (currentCustomizationString != outfitData.customizationString)
                                    {
                                        ApplyGlamourerEquipment(outfitData.customizationString);
                                        currentCustomizationString = outfitData.customizationString;
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }


        [Signature("4C 8B DC 55 53 41 56 49 8D 6B A1 48 81 EC F0 00 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 07", DetourName = nameof(CharacterStatusOnSetupDetour))]
        private readonly Hook<AddonOnSetup> characterStatusOnSetup = null!;

        private unsafe delegate void* AddonOnSetup(AtkUnitBase* atkUnitBase, int a2, void* a3);

        private unsafe void* CharacterStatusOnSetupDetour(AtkUnitBase* atkUnitBase, int a2, void* a3)
        {
            var val = characterStatusOnSetup.Original(atkUnitBase, a2, a3);
            try
            {
                CharacterStatusOnSetup(atkUnitBase);
            }
            catch (Exception e)
            {
                PluginLog.Log(e, "Failed to setup Character Panel");
            }
            return val;
        }

        internal unsafe void CharacterStatusOnSetup(AtkUnitBase* atkUnitBase)
        {
            PluginLog.Information("In Character Status On Setup");
            var uiState = UIState.Instance();

            var attributesPtr = atkUnitBase->UldManager.SearchNodeById(26);
            var mndNode = attributesPtr->ChildNode;

            var gearProp = atkUnitBase->UldManager.SearchNodeById(80);
            var avgItemLvlNode = gearProp->ChildNode;
            //avgItemLvlNode->PrevSiblingNode->ToggleVisibility(false); // header

            /*var _node = gearProp;
            PluginLog.Information(_node.ChildCount.ToString());
            var node = _node.ChildNode->PrevSiblingNode->ChildNode;
            if (node != null)
            {
                var textNode = node->GetAsAtkTextNode();
                PluginLog.Information($"{(textNode != null ? textNode->NodeText.ToString() : "")} - {node->GetType()} [{node->ChildCount.ToString()}]");
            }
            else
            {
                PluginLog.Information("null");
            }*/
            //PluginLog.Information($"{avgItemLvlNode->PrevSiblingNode->ParentNode[0].ChildNode->ToString()}, {(textNode != null ? textNode->NodeText : "null")}");
            /*expectedDamagePtr = AddStatRow((AtkComponentNode*)avgItemLvlNode, Localization.Panel_Damage_per_100_Potency, true);
            SetTooltip(expectedDamagePtr, Tooltips.Entry.ExpectedDamage);*/

            /*for (var i = 0; i < atkUnitBase->UldManager.NodeListCount; i++)
            {
                if (atkUnitBase->UldManager.NodeList[i] == null) continue;
                var node = atkUnitBase->UldManager.NodeList[i];
                var textNode = node->GetAsAtkTextNode();
                PluginLog.Information($"{i}, {(textNode != null ? textNode->NodeText.ToString() : "")} - {node->GetType()} [{node->ChildCount.ToString()}]");
                for (var b = 0; b < node->ChildCount; b++)
                {
                    var subNode = node[i];
                    var subTextNode = subNode.GetAsAtkTextNode();
                    PluginLog.Information($"    {b}, {(subTextNode != null ? subTextNode->NodeText.ToString() : "")} [{subNode.ChildCount.ToString()}]");
                }
            }*/

            TemperatureHandler.UpdateTemps();
            int hours = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Hour;
            int minutes = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Minute;
            int seconds = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Second;
            float calcHours = hours + ((float)minutes / 60) + ((float)seconds / 60 / 60);
            tooltips.ReloadTemperatureTooltip(Climates.GetTemperature(clientState.TerritoryType, calcHours) + TemperatureHandler.temperatureDivergence, Climates.weatherTemperatures.ContainsKey(*WeatherHandler.currentWeather) ? Climates.weatherTemperatures[*WeatherHandler.currentWeather] : Climates.weatherTemperatures[0]);

            var testRow = AddStatRow((AtkComponentNode*)avgItemLvlNode, "Temperature", false);
            var test2Row = AddStatRow((AtkComponentNode*)avgItemLvlNode, "Temperature2");
            testRow->SetText(TemperatureHandler.currentTemp.ToString() + "℉");
            test2Row->SetText(TemperatureHandler.currentTemp.ToString() + "C");
            SetTooltip((AtkComponentNode*)avgItemLvlNode, Tooltips.Entry.Hunger);
            SetTooltip(testRow, Tooltips.Entry.Temperature);
            SetTooltip(test2Row, Tooltips.Entry.Temperature);


            // Attempt to make new category (so far attempts have been unsuccessful)
            var size = sizeof(AtkComponentNode);
            var allocation = MemoryHelper.GameAllocateUi((ulong)size);
            var bytes = new byte[size];
            Marshal.Copy(new IntPtr(gearProp), bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, allocation, bytes.Length);

            var newNode = (AtkResNode*)allocation;
            newNode->ParentNode = null;
            newNode->ChildNode = null;
            newNode->ChildCount = 0;
            newNode->PrevSiblingNode = null;
            newNode->NextSiblingNode = null;
            test2Row->AtkResNode.ParentNode = newNode;
            //var testRow3 = AddStatRow((AtkComponentNode*)newNode->ChildNode, "Temperature", false);

            var characterStatusPtr = (IntPtr)atkUnitBase;

            //UpdateCharacterPanelForJob(job, lvl);
        }

        private unsafe void SetTooltip(AtkComponentNode* parentNode, Tooltips.Entry entry)
        {
            if (parentNode == null)
                return;
            var collisionNode = parentNode->Component->UldManager.RootNode;
            if (collisionNode == null)
                return;

            var ttMgr = AtkStage.GetSingleton()->TooltipManager;
            var ttMsg = Find(ttMgr.TooltipMap.Head->Parent, collisionNode).Value;
            ttMsg->AtkTooltipArgs.Text = (byte*)tooltips[entry];
        }

        private unsafe void SetTooltip(AtkTextNode* node, Tooltips.Entry entry)
        {
            SetTooltip((AtkComponentNode*)node->AtkResNode.ParentNode, entry);
        }

        private unsafe AtkTextNode* AddStatRow(AtkComponentNode* parentNode, string label, bool hideOriginal = false)
        {
            ExpandNodeList(parentNode, 2);
            var collisionNode = parentNode->Component->UldManager.RootNode;
            if (!hideOriginal)
            {
                parentNode->AtkResNode.Height += 20;
                collisionNode->Height += 20;
            }

            var numberNode = (AtkTextNode*)collisionNode->PrevSiblingNode;
            var labelNode = (AtkTextNode*)numberNode->AtkResNode.PrevSiblingNode;
            var newNumberNode = CloneNode(numberNode);
            var prevSiblingNode = labelNode->AtkResNode.PrevSiblingNode;
            labelNode->AtkResNode.PrevSiblingNode = (AtkResNode*)newNumberNode;
            newNumberNode->AtkResNode.ParentNode = (AtkResNode*)parentNode;
            newNumberNode->AtkResNode.NextSiblingNode = (AtkResNode*)labelNode;
            newNumberNode->AtkResNode.Y = parentNode->AtkResNode.Height - 24;
            newNumberNode->TextColor = new ByteColor { A = 0xFF, R = 0xA0, G = 0xA0, B = 0xA0 };
            newNumberNode->NodeText.StringPtr = (byte*)MemoryHelper.GameAllocateUi((ulong)newNumberNode->NodeText.BufSize);
            parentNode->Component->UldManager.NodeList[parentNode->Component->UldManager.NodeListCount++] = (AtkResNode*)newNumberNode;
            var newLabelNode = CloneNode(labelNode);
            newNumberNode->AtkResNode.PrevSiblingNode = (AtkResNode*)newLabelNode;
            newLabelNode->AtkResNode.ParentNode = (AtkResNode*)parentNode;
            newLabelNode->AtkResNode.PrevSiblingNode = prevSiblingNode;
            newLabelNode->AtkResNode.NextSiblingNode = (AtkResNode*)newNumberNode;
            newLabelNode->AtkResNode.Y = parentNode->AtkResNode.Height - 24;
            newLabelNode->TextColor = new ByteColor { A = 0xFF, R = 0xA0, G = 0xA0, B = 0xA0 };
            newLabelNode->NodeText.StringPtr = (byte*)MemoryHelper.GameAllocateUi((ulong)newLabelNode->NodeText.BufSize);
            newLabelNode->SetText(label);
            parentNode->Component->UldManager.NodeList[parentNode->Component->UldManager.NodeListCount++] = (AtkResNode*)newLabelNode;
            if (hideOriginal)
            {
                labelNode->AtkResNode.ToggleVisibility(false);
                numberNode->TextColor.A = 0; // toggle visibility doesn't work since it's constantly updated by the game
            }

            return newNumberNode;
        }

        private unsafe void ExpandNodeList(AtkComponentNode* componentNode, ushort addSize)
        {
            var originalList = componentNode->Component->UldManager.NodeList;
            var originalSize = componentNode->Component->UldManager.NodeListCount;
            var newSize = (ushort)(componentNode->Component->UldManager.NodeListCount + addSize);
            var oldListPtr = new IntPtr(originalList);
            var newListPtr = MemoryHelper.GameAllocateUi((ulong)((newSize + 1) * 8));
            var clone = new IntPtr[originalSize];
            Marshal.Copy(oldListPtr, clone, 0, originalSize);
            Marshal.Copy(clone, 0, newListPtr, originalSize);
            componentNode->Component->UldManager.NodeList = (AtkResNode**)newListPtr;
        }

        private static unsafe AtkTextNode* CloneNode(AtkTextNode* original)
        {
            var size = sizeof(AtkTextNode);
            var allocation = MemoryHelper.GameAllocateUi((ulong)size);
            var bytes = new byte[size];
            Marshal.Copy(new IntPtr(original), bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, allocation, bytes.Length);

            var newNode = (AtkResNode*)allocation;
            newNode->ParentNode = null;
            newNode->ChildNode = null;
            newNode->ChildCount = 0;
            newNode->PrevSiblingNode = null;
            newNode->NextSiblingNode = null;
            return (AtkTextNode*)newNode;
        }
        private readonly Tooltips tooltips;
        private unsafe TVal Find<TKey, TVal>(StdMap<Pointer<TKey>, TVal>.Node* node, TKey* item) where TKey : unmanaged where TVal : unmanaged
        {
            while (!node->IsNil)
            {
                if (node->KeyValuePair.Item1.Value < item)
                {
                    node = node->Right;
                    continue;
                }

                if (node->KeyValuePair.Item1.Value > item)
                {
                    node = node->Left;
                    continue;
                }

                return node->KeyValuePair.Item2;
            }

            return default;
        }
    }
}
