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

namespace RPToolkit
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {
        public System.Drawing.Rectangle gameDimensions { get; private set; } = new System.Drawing.Rectangle();
        [PluginService] static internal SigScanner SigScanner { get; private set; }
        [PluginService] static internal DataManager Data { get; private set; }
        [PluginService] static internal Framework Framework { get; private set; }
        [PluginService] static internal Dalamud.Game.ClientState.Conditions.Condition Condition { get; private set; }
        public static CommandManager CommandManager { get; private set; } = null!;
        public static GameGui GameGui { get; private set; } = null!;
        private readonly ClientState clientState;
        public static ChatGui chat { get; private set; } = null!;
        public static UiBuilder UiBuilder { get; private set; } = null!;

        internal IntPtr TimeAsmPtr;
        internal long* TrueTime;
        private uint* Time;
        private int currentTemperatureStage = 0;
        public int currentTemp { get; private set; }
        private int secPassed = 0;
        private int secUntilDivergenceUpdate = 300;
        private int temperatureDivergence = 0;
        private int temperatureDivergenceLimit = 5;
        private bool updateCharacterPanel = false;

        internal byte* currentWeather;
        internal byte prevWeather;
        internal Dictionary<byte, string> weathers;
        internal byte[] rainWeathers = { 7, 9, 10, 22, 57, 58, 88, 62, 64, 143 };
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
        private Command[] commands = {
            new Command("/rainwindow", "Open the rain popup window, for testing purposes."), 
            new Command("/checkweather", "Checks the weather, for testing purposes."),
            new Command("/umbrella", "Takes out your umbrella."),
            new Command("/pickpocket", "Pickpockets your target for a random amount of gil.")
        };

        public DalamudPluginInterface PluginInterface { get; private set; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("RPToolkit");

        // I frickin suck at writing comments, so to anyone reading through this, good luck soldier and god speed
        public Plugin(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        CommandManager commandManager,
        GameGui gameGui,
        ClientState clientState,
        ChatGui chatGui)
        {
            this.PluginInterface = pluginInterface;
            CommandManager = commandManager;
            GameGui = gameGui;
            this.clientState = clientState;
            UiBuilder = this.PluginInterface.UiBuilder;
            chat = chatGui;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            GetGameDimensions();

            var world = this.clientState.LocalPlayer?.CurrentWorld.GameData.Name.RawString;
            PluginLog.Information($"Hello {world}!");

            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new MainWindow(this));
            WindowSystem.AddWindow(new NoRainWindow(this));

            foreach (Command cmd in commands)
            {
                CommandManager.AddHandler(cmd.command, new CommandInfo(OnCommand)
                {
                    HelpMessage = cmd.description
                });
            }

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            TrueTime = (long*)(Framework.Address.BaseAddress + 0x1770);
            TimeAsmPtr = SigScanner.ScanText("48 89 5C 24 ?? 57 48 83 EC 30 4C 8B 15") + 0x19;
            Time = (uint*)(TimeAsmPtr + 0x3);

            weathers = Data.GetExcelSheet<Weather>().ToDictionary(row => (byte)row.RowId, row => row.Name.ToString());
            currentWeather = (byte*)(*(IntPtr*)SigScanner.GetStaticAddressFromSig("48 8B 05 ?? ?? ?? ?? 48 83 C1 10 48 89 74 24") + 0x26);
            PluginLog.Information($"Weather ptr: {(IntPtr)currentWeather:X16}");

            ChatHelper.Initialize();

            //would use .net 6 PeriodicTimer but it can't be used in unsafe contexts, research for later
            tick.AutoReset = true;
            tick.Elapsed += CheckWeather;
            tick.Elapsed += UpdateTemps;
            tick.Elapsed += GetGameDimensions;
            tick.Start();


            if (updateCharacterPanel)
            {
                tooltips = new Tooltips();
                SignatureHelper.Initialise(this);
                characterStatusOnSetup.Enable();
            }
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

        public void Dispose()
        {
            tick.Dispose();
            this.WindowSystem.RemoveAllWindows();
            foreach (Command cmd in commands)
            {
                CommandManager.RemoveHandler(cmd.command);
            }

            characterStatusOnSetup.Dispose();
            tooltips.Dispose();
        }

        private void OnCommand(string command, string args)
        {
            PluginLog.Information(command);
            switch (command)
            {
                case "/checkweather":
                    PluginLog.Information($"Weather is currently: {*currentWeather}");
                    PluginLog.Information($"Named weather shows: {weathers[*currentWeather]}");
                    CheckWeather();
                    break;
                case "/rainwindow":
                    // in response to the slash command, just display our main ui
                    WindowSystem.GetWindow("No Rain Prompt").IsOpen = true;
                    break;
                case "/umbrella":
                    ChatHelper.SendChatMessage("/fashion \"Prim Dot Parasol\"");
                    break;
                case "/pickpocket":
                    Random rnd = new Random();
                    ChatHelper.SendChatMessage($"/emote pickpockets <t> for {String.Format("{0:n0}", rnd.Next(Configuration.minPickpocketAmt,Configuration.maxPickpocketAmt))}.");
                    break;
                default:
                    break;
            }
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            WindowSystem.GetWindow($"{this.Name} Config Window").IsOpen = true;
        }

        private void CheckWeather(Object source, System.Timers.ElapsedEventArgs e)
        {
            CheckWeather();
        }

        public void CheckWeather()
        {
            //PluginLog.Information(this.clientState.TerritoryType.ToString());
            //PluginLog.Information(Convert.ToString((uint)Time));

            //PluginLog.Information("True time: " + *TrueTime + " / " + DateTimeOffset.FromUnixTimeSeconds(*TrueTime).ToString());
            //PluginLog.Information($"{DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Hour:00}:{DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Minute:00}:{DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Second:00}");
            
            //PluginLog.Information($"{calcHours.ToString()} - {Climates.GetTemperature(this.clientState.TerritoryType, calcHours).ToString()}");

            if (Configuration.enableRainPopup && !Condition[ConditionFlag.OccupiedInCutSceneEvent] && !Condition[ConditionFlag.WatchingCutscene78]) {
                if (*currentWeather != prevWeather)
                {
                    if (rainWeathers.Contains(*currentWeather) && !rainWeathers.Contains(prevWeather) && !Condition[ConditionFlag.UsingParasol])
                    {
                        ChatHelper.Echo("Gentle raindrops begin to fall upon your skin.", Configuration.temperatureChatType, "Weather");
                        WindowSystem.GetWindow("No Rain Prompt").IsOpen = false;
                        WindowSystem.GetWindow("Rain Prompt").IsOpen = true;
                    }
                    else if (!rainWeathers.Contains(*currentWeather) && rainWeathers.Contains(prevWeather))
                    {
                        ChatHelper.Echo("The rain begins to clear.", Configuration.temperatureChatType, "Weather");
                        WindowSystem.GetWindow("Rain Prompt").IsOpen = false;
                        if (Condition[ConditionFlag.UsingParasol])
                        {
                            WindowSystem.GetWindow("No Rain Prompt").IsOpen = true;
                        }
                    }

                    prevWeather = *currentWeather;
                }
            }
        }

        public void UpdateTemps(Object source, System.Timers.ElapsedEventArgs e)
        {
            UpdateTemps();
        }

        public void UpdateTemps()
        {
            if (Configuration.enableTemperatureMessages && Climates.zoneTemperatures.ContainsKey(this.clientState.TerritoryType))
            {
                secPassed++;
                if (secPassed >= secUntilDivergenceUpdate)
                {
                    secPassed = 0;
                    Random rnd = new Random();
                    int temperatureAdjustment = temperatureDivergence + rnd.Next(-1, 2);
                    if (temperatureAdjustment >= -temperatureDivergenceLimit && temperatureAdjustment <= temperatureDivergenceLimit)
                    {
                        temperatureDivergence = temperatureAdjustment;
                    }
                }

                int hours = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Hour;
                int minutes = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Minute;
                int seconds = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Second;
                float calcHours = hours + ((float)minutes / 60) + ((float)seconds / 60 / 60);
                currentTemp =
                    Climates.GetTemperature(this.clientState.TerritoryType, calcHours) +
                    (Climates.weatherTemperatures.ContainsKey(*currentWeather) ?
                        Climates.weatherTemperatures[*currentWeather]
                    :
                        Climates.weatherTemperatures[0]
                    ) +
                    temperatureDivergence;

                PluginLog.Information($"{currentTemp.ToString()} ({Climates.GetTemperature(this.clientState.TerritoryType, calcHours)}, {temperatureDivergence}, {(Climates.weatherTemperatures.ContainsKey(*currentWeather) ? Climates.weatherTemperatures[*currentWeather] : Climates.weatherTemperatures[0])})");

                int newStage = currentTemperatureStage;
                foreach (KeyValuePair<int, Climates.TemperatureDescription> tempStages in Climates.temperatureStages)
                {
                    if (currentTemp < currentTemperatureStage)
                    {
                        if (currentTemp >= tempStages.Key && tempStages.Key < currentTemperatureStage)
                        {
                            newStage = tempStages.Key;
                        }
                    }
                    else if (currentTemp > currentTemperatureStage)
                    {
                        if (tempStages.Key > currentTemperatureStage && currentTemp >= tempStages.Key)
                        {
                            newStage = tempStages.Key;
                        }
                    }
                }
                if (currentTemperatureStage != newStage)
                {
                    if (newStage < currentTemperatureStage)
                        ChatHelper.Echo(Climates.temperatureStages[newStage].decreaseDesc, Configuration.temperatureChatType, "Temperature");
                    else if (newStage > currentTemperatureStage)
                        ChatHelper.Echo(Climates.temperatureStages[newStage].increaseDesc, Configuration.temperatureChatType, "Temperature");
                    currentTemperatureStage = newStage;
                }
            }
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

        private void GetGameDimensions(Object source, System.Timers.ElapsedEventArgs e)
        {
            GetGameDimensions();
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

            UpdateTemps();
            int hours = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Hour;
            int minutes = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Minute;
            int seconds = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Second;
            float calcHours = hours + ((float)minutes / 60) + ((float)seconds / 60 / 60);
            tooltips.ReloadTemperatureTooltip(Climates.GetTemperature(this.clientState.TerritoryType, calcHours) + temperatureDivergence, Climates.weatherTemperatures.ContainsKey(*currentWeather) ? Climates.weatherTemperatures[*currentWeather] : Climates.weatherTemperatures[0]);

            var testRow = AddStatRow((AtkComponentNode*)avgItemLvlNode, "Temperature", false);
            var test2Row = AddStatRow((AtkComponentNode*)avgItemLvlNode, "Temperature2");
            testRow->SetText(currentTemp.ToString() + "℉");
            test2Row->SetText(currentTemp.ToString() + "C");
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
