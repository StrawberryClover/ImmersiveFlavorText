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

namespace RPToolkit
{
    public unsafe sealed class Plugin : IDalamudPlugin
    {
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
        private int currentTemp;
        private int secPassed = 0;
        private int secUntilDivergenceUpdate = 180;
        private int tempDivergence = 0;
        private int tempDivergenceLimit = 5;
        private int tempRainModifier;

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
        //public CommandManager CommandManager { get; private set; } = null!;
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("RPToolkit");

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


            var world = this.clientState.LocalPlayer?.CurrentWorld.GameData.Name.RawString;
            PluginLog.Information($"Hello {world}!");

            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new MainWindow(this));

            foreach(Command cmd in commands)
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
            tick.AutoReset = true; // the key is here so it repeats
            tick.Elapsed += CheckWeather;
            tick.Elapsed += UpdateTemps;
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

        public void Dispose()
        {
            tick.Dispose();
            this.WindowSystem.RemoveAllWindows();
            foreach (Command cmd in commands)
            {
                CommandManager.RemoveHandler(cmd.command);
            }
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
                    WindowSystem.GetWindow("Rain Prompt").IsOpen = true;
                    break;
                case "/umbrella":
                    ChatHelper.SendChatMessage("/fashion \"Prim Dot Parasol\"");
                    break;
                case "/pickpocket":
                    Random rnd = new Random();
                    ChatHelper.SendChatMessage($"/emote pickpockets <t> for {String.Format("{0:n0}", rnd.Next(1,20000))}.");
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
                        ChatHelper.Echo("Gentle raindrops begin to fall upon your skin.");
                        WindowSystem.GetWindow("Rain Prompt").IsOpen = true;
                    }
                    else if (!rainWeathers.Contains(*currentWeather) && rainWeathers.Contains(prevWeather))
                    {
                        ChatHelper.Echo("The rain begins to clear.");
                    }

                    prevWeather = *currentWeather;
                }
            }
        }

        public void UpdateTemps(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Configuration.enableTemperatureMessages && Climates.temperatures.ContainsKey(this.clientState.TerritoryType))
            {
                secPassed++;
                if (secPassed >= secUntilDivergenceUpdate)
                {
                    secPassed = 0;
                    Random rnd = new Random();
                    int tempAdjustment = tempDivergence + rnd.Next(-1, 2);
                    if (tempAdjustment >= -tempDivergenceLimit && tempAdjustment <= tempDivergenceLimit)
                    {
                        tempDivergence = tempAdjustment;
                    }
                }

                if (rainWeathers.Contains(*currentWeather) && !Condition[ConditionFlag.UsingParasol])
                    tempRainModifier = -5;
                else
                    tempRainModifier = 0;

                int hours = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Hour;
                int minutes = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Minute;
                int seconds = DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Second;
                float calcHours = hours + ((float)minutes / 60) + ((float)seconds / 60 / 60);
                currentTemp = Climates.GetTemperature(this.clientState.TerritoryType, calcHours) + tempDivergence + tempRainModifier;
                //PluginLog.Information($"{currentTemp.ToString()} ({Climates.GetTemperature(this.clientState.TerritoryType, calcHours)}+{tempDivergence})");

                int newStage = currentTemperatureStage;
                foreach(KeyValuePair<int, Climates.TemperatureDescription> tempStages in Climates.temperatureStages)
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
                        ChatHelper.Echo(Climates.temperatureStages[newStage].decreaseDesc);
                    else if (newStage > currentTemperatureStage)
                        ChatHelper.Echo(Climates.temperatureStages[newStage].increaseDesc);
                    currentTemperatureStage = newStage;
                }
            }
        }
    }
}
