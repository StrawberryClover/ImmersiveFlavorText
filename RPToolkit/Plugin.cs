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
using Lumina.Extensions;
using FFXIVClientStructs.FFXIV.Common.Math;
using CSFramework = FFXIVClientStructs.FFXIV.Client.System.Framework.Framework;
using FFXIVClientStructs.FFXIV.Common.Component.BGCollision;
using FFXIVClientStructs.FFXIV.Client.Game.Event;
using FFXIVClientStructs.FFXIV.Client.Graphics.Kernel;
using System.Drawing;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using RPToolkit.Localizations;
using Localization = RPToolkit.Localizations.Localization;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Game;
using GameObject = Dalamud.Game.ClientState.Objects.Types.GameObject;
using RPToolkit.Data;
using System.Xml.Linq;

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

        public readonly ObjectTable _objectTable;
        private readonly ICallGateSubscriber<int> _glamourerApiVersion;
        private readonly ICallGateSubscriber<string, GameObject?, object>? _glamourerApplyOnlyEquipment;
        private readonly ICallGateSubscriber<GameObject?, string>? _glamourerGetAllCustomization;
        private string currentCustomizationString;
        public bool glamourerAvailable { get; private set; }

        public static TerritoryInfo* AreaInfo => TerritoryInfo.Instance();

        System.Timers.Timer tick = new System.Timers.Timer(1000);

        public string Name => "Immersive Flavor Text";

        public DalamudPluginInterface PluginInterface { get; private set; }
        public Configuration _configuration { get; init; }
        public static Configuration Configuration { get { return Plugin.Singleton._configuration; } private set { } }
        public static WindowSystem WindowSystem = new("RPToolkit");

        private uint previousTerritory;

        private uint previousJobChecked;
        public event EventHandler<uint> JobChanged;
        

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
            Localizations.Localization.LoadLocalization();
            TimeHelper.Initialize(pluginInterface);
            MovementHelper.Initialize(pluginInterface);
            CommandHandler.Initialize(pluginInterface);
            WeatherHandler.Initialize(pluginInterface);
            CharacterWindowUIHandler.Initialize(pluginInterface);
            ConsumableActionHandler.Initialize(pluginInterface);
            #endregion

            #region Other Plugin API Initialization
            _glamourerApiVersion = pluginInterface.GetIpcSubscriber<int>("Glamourer.ApiVersion");
            _glamourerApplyOnlyEquipment = pluginInterface.GetIpcSubscriber<string, GameObject?, object>("Glamourer.ApplyOnlyEquipmentToCharacter");
            _glamourerGetAllCustomization = pluginInterface.GetIpcSubscriber<GameObject?, string>("Glamourer.GetAllCustomizationFromCharacter");
            #endregion

            GetGameDimensions();

            #region Windows Initialization
            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new TempSuggestionWindow(this));
            WindowSystem.AddWindow(new DebugOverlay(this));
            #endregion

            this.PluginInterface.UiBuilder.Draw += WindowSystem.Draw;
            this.PluginInterface.UiBuilder.OpenConfigUi += () => { ConfigWindow.window.IsOpen = true; };

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
            if (this.clientState.LocalPlayer != null) OnTerritoryChange();
            this.clientState.TerritoryChanged += OnTerritoryChange;
            this.clientState.Login += OnLogin;
            Framework.Update += OnFrameworkUpdate;

            if (clientState.LocalPlayer != null)
                OnLogin();

            /*GameObjectArray[] objectArrays =
            {
                TargetSystem.Instance()->ObjectFilterArray0,
                TargetSystem.Instance()->ObjectFilterArray1,
                TargetSystem.Instance()->ObjectFilterArray2,
                TargetSystem.Instance()->ObjectFilterArray3,
            };
            foreach (GameObjectArray goArray in objectArrays)
            {
                for (int i = 0; i < goArray.Length; i++)
                {
                    var gameObject = goArray[i];
                    PluginLog.Information(gameObject->GetName()->ToString() + " (" + gameObject->ObjectKind.ToString() + ")");
                    gameObjectsTest.Add(*gameObject);
                }
            }*/

            if (pluginInterface.IsDev)
            {
                Condition.ConditionChange += OnConditionChange;
                //DebugOverlay.window.IsOpen = true;
            }

            JobChanged += OnJobChange;

            Dictionary<string, int> flagResults = new Dictionary<string, int>()
            {
                {"A", 0 },
                {"B", 0 },
                {"C", 0 },
                {"D", 0 },
                {"E", 0 },
                {"F", 0 },
                {"G", 0 },
                {"H", 0 },
                {"I", 0 },
            };
            for (int i = 0; i < 10; i++)
            {
                string flagsSet = "";
                Random rnd = new Random();
                int randomFlags = rnd.Next(0, Enum.GetValues<TestFlags>().Cast<int>().Sum());

                if ((randomFlags & (int)TestFlags.A) == (int)TestFlags.A)
                {
                    flagResults["A"]++;
                    flagsSet += "A";
                }
                if ((randomFlags & (int)TestFlags.B) == (int)TestFlags.B)
                {
                    flagResults["B"]++;
                    flagsSet += "B";
                }
                if ((randomFlags & (int)TestFlags.C) == (int)TestFlags.C)
                {
                    flagResults["C"]++;
                    flagsSet += "C";
                }
                if ((randomFlags & (int)TestFlags.D) == (int)TestFlags.D)
                {
                    flagResults["D"]++;
                    flagsSet += "D";
                }
                if ((randomFlags & (int)TestFlags.E) == (int)TestFlags.E)
                {
                    flagResults["E"]++;
                    flagsSet += "E";
                }
                if ((randomFlags & (int)TestFlags.F) == (int)TestFlags.F)
                {
                    flagResults["F"]++;
                    flagsSet += "F";
                }
                if ((randomFlags & (int)TestFlags.G) == (int)TestFlags.G)
                {
                    flagResults["G"]++;
                    flagsSet += "G";
                }
                if ((randomFlags & (int)TestFlags.H) == (int)TestFlags.H)
                {
                    flagResults["H"]++;
                    flagsSet += "H";
                }
                if ((randomFlags & (int)TestFlags.I) == (int)TestFlags.I)
                {
                    flagResults["I"]++;
                    flagsSet += "I";
                }
                PluginLog.Information($"Flags Set: {flagsSet}");
            }

            PluginLog.Information("Results");
            foreach (KeyValuePair<string, int> kv in flagResults.OrderBy(a => a.Key))
            {
                PluginLog.Information($"{kv.Key}: {kv.Value}");
            }
        }

        private void OnJobChange(object? sender, uint e)
        {
            currentCustomizationString = "";
        }

        [Flags]
        public enum TestFlags
        {
            A = 1 << 0,
            B = 1 << 1,
            C = 1 << 2,
            D = 1 << 3,
            E = 1 << 4,
            F = 1 << 5,
            G = 1 << 6,
            H = 1 << 7,
            I = 1 << 8
        }

        public void OnFrameworkUpdate(Framework framework)
        {
            if (this.clientState.LocalPlayer && this.clientState.LocalPlayer.ClassJob.Id != previousJobChecked)
            {
                JobChanged?.Invoke(this, this.clientState.LocalPlayer.ClassJob.Id);
                previousJobChecked = this.clientState.LocalPlayer.ClassJob.Id;
            }
        }


        private float GetDistanceToObject(GameObject target)
        {
            var distance = Vector3.Distance((Vector3)clientState.LocalPlayer?.Position, target.Position);
            //distance -= source.HitboxRadius;
            distance -= target.HitboxRadius;
            return distance;
        }

        private void OnConditionChange(ConditionFlag flag, bool value)
        {
            if (PluginInterface.IsDev)
                PluginLog.Information(flag.ToString() + " " + value.ToString());
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

            PluginLog.Information($"Dev version: {PluginInterface.IsDev}");
            if (PluginInterface.IsDev)
            {
                tick.Elapsed += DebugStuff;
            }

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
            CharacterWindowUIHandler.Dispose();
            ConsumableActionHandler.Dispose();

            AppDomain.CurrentDomain.FirstChanceException -= HandleException;
            this.clientState.TerritoryChanged -= OnTerritoryChange;
            this.clientState.Login -= OnLogin;
            if (PluginInterface.IsDev)
            {
                Condition.ConditionChange -= OnConditionChange;
            }
            Framework.Update -= OnFrameworkUpdate;
        }

        private void DebugStuff(System.Object? source, System.Timers.ElapsedEventArgs e)
        {
            //var wetness = *((byte*)this.clientState.LocalPlayer.Address + 0x1B1F);
            //*((byte*)this.clientState.LocalPlayer.Address + 0x1B1F) = 36;
            //var wetness = (int*)(clientState.LocalPlayer.Address + 0x2B0);
            //PluginLog.Information(wetness.ToString());
            //PluginLog.Information(Control.Instance()->LocalPlayer->Character.CustomizeData);
            //var charObject = Control.Instance()->LocalPlayer->Character.GameObject..ToString();
            //var objectType = charObject.GetObjectType();
            //PluginLog.Information(charObject);
            //var baseObject = Convert.ChangeType(charObject, objectType);
            //PluginLog.Information(objectType.ToString() + ": " + baseObject.ToString());
            //var c = (FFXIVClientStructs.FFXIV.Client.Graphics.Scene.CharacterBase*)this.clientState.LocalPlayer.Address;
            //PluginLog.Information($"({c->GetModelType()}) {c->WetnessDepth} : {c->ForcedWetness} : {c->SwimmingWetness} : {c->WeatherWetness}");
            //var walking = (IntPtr*)SigScanner.GetStaticAddressFromSig("40 38 35 ?? ?? ?? ?? 75 2D");
            //PluginLog.Information($"Walking: {walking->ToString()}");
            //*walking = 1;
            //MovementHelper.SetWalking(!MovementHelper.isWalking);
            //PluginLog.Information($"Walking: {MovementHelper.isWalking}");
        }

        private void GetGameDimensions()
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool GetWindowRect(IntPtr hWnd, out System.Drawing.Rectangle lpRect);

            // temporary variable since you cannot output to this.gameDimensions
            System.Drawing.Rectangle d;
            GetWindowRect(Process.GetCurrentProcess().MainWindowHandle, out d);
            this.gameDimensions = d;
        }

        private void GetGameDimensions(System.Object? source, System.Timers.ElapsedEventArgs e)
        {
            GetGameDimensions();
        }

        private void OnTerritoryChange(object? sender, ushort e) { OnTerritoryChange(); }
        private void OnTerritoryChange()
        {
            TemperatureHandler.NewZone();
            currentCustomizationString = "";

            if (Configuration.walkWhenEnteringHouse && IsInHouse()) MovementHelper.SetWalking(true);
        }

        private void OnLogin(object? sender, EventArgs e) { OnLogin(); }
        private void OnLogin()
        {
            var world = clientState.LocalPlayer?.CurrentWorld.GameData.Name.RawString;
            var dataCenter = Data.GetExcelSheet<World>()?.GetRow((uint)clientState.LocalPlayer?.CurrentWorld.Id).DataCenter.Value.Name.RawString;
            PluginLog.Information($"Hello ({dataCenter}) {world}!");

            if (!Configuration.useCelsius.HasValue)
            {
                string[] americanDataCenters = { "aether", "crystal", "dynamis", "primal" };
                if (americanDataCenters.Contains(dataCenter.ToLower()))
                {
                    Configuration.useCelsius = false;
                }
                else
                {
                    Configuration.useCelsius = true;
                }
                Configuration.Save();
            }

            OnTerritoryChange();
        }

        internal bool IsPlayerOccupied()
        {
            return (Condition[ConditionFlag.OccupiedInCutSceneEvent] ||
                Condition[ConditionFlag.WatchingCutscene78] ||
                Condition[ConditionFlag.BetweenAreas] ||
                Condition[ConditionFlag.BetweenAreas51]);
        }

        internal bool IsInHouse()
        {
            ushort[] housingAreas = { 
                282, 283, 284, 573, 608, //Mist
                342, 343, 344, 574, 609, //Lavender Beds
                345, 346, 347, 575, 610, //The Goblet
                649, 650, 651, 654, 655, //Shirogane
                980, 981, 982, 985, 999 //Ishgard
            };
            if (housingAreas.Contains(clientState.TerritoryType))
            {
                return true;
            }
            else return false;
        }

        private void PeriodicApiStateCheck(System.Object? source, System.Timers.ElapsedEventArgs e)
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

        private void PeriodicOutfitChangeCheck(System.Object? source, System.Timers.ElapsedEventArgs e)
        {
            if (glamourerAvailable && !IsPlayerOccupied() && !Condition[ConditionFlag.Fishing])
            {
                string glamourerData = GetGlamourerCurrentEquipment();
                string newCustomizationString = glamourerData;
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

                            if (outfitData.climateConditions == (int)ClimateOutfitData.ClimateConditions.Only_When_Raining) //set to only swimming
                            {
                                PluginLog.Information("Outfit1");
                                ChangeOutfit(outfitData.customizationString);
                                return;
                            }
                        }
                        else
                            correctWeather = true;

                        bool correctSwimmingCondition = false;
                        if ((outfitData.climateConditions & (int)ClimateOutfitData.ClimateConditions.Swimming) == (int)ClimateOutfitData.ClimateConditions.Swimming)
                        {
                            if (Condition[ConditionFlag.Swimming] || Condition[ConditionFlag.Diving])
                            {
                                correctSwimmingCondition = true;

                                if (outfitData.climateConditions == (int)ClimateOutfitData.ClimateConditions.Swimming) //set to only swimming
                                {
                                    PluginLog.Information("Outfit2");
                                    ChangeOutfit(outfitData.customizationString);
                                    return;
                                }
                            }
                        }
                        else
                            correctSwimmingCondition = true;

                        if (correctWeather && correctSwimmingCondition)
                        {
                            //PluginLog.Information(currentTemperatureStage.ToString());
                            //PluginLog.Information(Climates.temperatureStages.ElementAt(0).Key);
                            bool correctTemperature = false;
                            for (int i = 1; i < Enum.GetValues(typeof(ClimateOutfitData.ClimateConditions)).Length; i++)
                            {
                                int value = ((int[])Enum.GetValues(typeof(ClimateOutfitData.ClimateConditions)))[i];
                                if ((outfitData.climateConditions & value) == value)
                                {
                                    if (TemperatureHandler.currentTemperatureStage == Climates.temperatureStages.ElementAt((Climates.temperatureStages.Count - 1) - (i - 1)).Key)
                                    {
                                        if (PluginInterface.IsDev)
                                            PluginLog.Information(i + ": " + Enum.GetName(typeof(ClimateOutfitData.ClimateConditions), value) + " (" + Climates.temperatureStages.ElementAt((Climates.temperatureStages.Count - 1) - (i - 1)).Key + ")");

                                        newCustomizationString = outfitData.customizationString;
                                        correctTemperature = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                PluginLog.Information("Outfit3");
                if (newCustomizationString != currentCustomizationString && glamourerData != newCustomizationString) ChangeOutfit(newCustomizationString);
            }
        }

        private void ChangeOutfit(string customizationString)
        {
            if (currentCustomizationString != customizationString)
            {
                PluginLog.Information($"Changing Outfit Data: {customizationString}");
                ApplyGlamourerEquipment(customizationString);
                currentCustomizationString = customizationString;
            }
        }
    }
}
