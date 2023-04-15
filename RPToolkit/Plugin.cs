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
            Localizations.Localization.LoadLocalization();
            TimeHelper.Initialize(pluginInterface);
            CommandHandler.Initialize(pluginInterface);
            WeatherHandler.Initialize(pluginInterface);
            CharacterWindowUIHandler.Initialize(pluginInterface);
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
            this.clientState.Login += OnTerritoryChange;
            Framework.Update += OnFrameworkUpdate;

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
        }

        public void OnFrameworkUpdate(Framework framework)
        {

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

            AppDomain.CurrentDomain.FirstChanceException -= HandleException;
            this.clientState.TerritoryChanged -= OnTerritoryChange;
            this.clientState.Login -= OnTerritoryChange;
            if (PluginInterface.IsDev)
            {
                Condition.ConditionChange -= OnConditionChange;
            }
            Framework.Update -= OnFrameworkUpdate;
        }

        private void DebugStuff(System.Object? source, System.Timers.ElapsedEventArgs e)
        {
            PluginLog.Information($"\r\n({clientState.TerritoryType}) \"{Data.GetExcelSheet<TerritoryType>()?.GetRow(clientState.TerritoryType)!.PlaceName.Value!.Name.RawString}\" " +
                $"\r\n> ({AreaInfo->AreaPlaceNameID}) \"{Data.GetExcelSheet<PlaceName>()?.GetRow(AreaInfo->AreaPlaceNameID).NameNoArticle}\" " +
                $"\r\n> ({AreaInfo->SubAreaPlaceNameID}) \"{Data.GetExcelSheet<PlaceName>()?.GetRow(AreaInfo->SubAreaPlaceNameID).NameNoArticle}\" " +
                $"\r\n Weather: ({*WeatherHandler.currentWeather}) {Data.GetExcelSheet<Weather>()?.GetRow(*WeatherHandler.currentWeather).Name}");

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

        private void OnTerritoryChange()
        {
            TemperatureHandler.NewZone();
        }
        private void OnTerritoryChange(object? sender, ushort e)
        {
            OnTerritoryChange();
        }
        private void OnTerritoryChange(object? sender, EventArgs e)
        {
            OnTerritoryChange();
        }

        internal bool IsPlayerOccupied()
        {
            return (Condition[ConditionFlag.OccupiedInCutSceneEvent] ||
                Condition[ConditionFlag.WatchingCutscene78] ||
                Condition[ConditionFlag.BetweenAreas] ||
                Condition[ConditionFlag.BetweenAreas51]);
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
    }
}
