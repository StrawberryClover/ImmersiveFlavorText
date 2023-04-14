using Dalamud.Game;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Logging;
using Dalamud.Plugin;
using Lumina.Excel.GeneratedSheets;
using RPToolkit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Object = System.Object;

namespace RPToolkit.Handlers
{
    public unsafe class WeatherHandler
    {
        public static void Initialize(DalamudPluginInterface pluginInterface)
        => pluginInterface.Create<WeatherHandler>();

        internal static byte* currentWeather;
        internal static byte prevWeather;
        internal static Dictionary<byte, string> weathers;
        internal static byte[] rainWeathers = { 7, 10, 22, 57, 58, 88, 62, 64, 143 };
        public static bool isRaining = false;

        public WeatherHandler()
        {
            weathers = Plugin.Data.GetExcelSheet<Weather>().ToDictionary(row => (byte)row.RowId, row => row.Name.ToString());
            currentWeather = (byte*)(*(IntPtr*)Plugin.SigScanner.GetStaticAddressFromSig("48 8B 05 ?? ?? ?? ?? 48 83 C1 10 48 89 74 24") + 0x26);
            PluginLog.Information($"Weather ptr: {(IntPtr)currentWeather:X16}");
        }

        public static void CheckWeather(Object? source, System.Timers.ElapsedEventArgs e)
        {
            CheckWeather();
        }

        public static void CheckWeather()
        {
            //PluginLog.Information(this.clientState.TerritoryType.ToString());
            //PluginLog.Information(Convert.ToString((uint)Time));

            //PluginLog.Information("True time: " + *TrueTime + " / " + DateTimeOffset.FromUnixTimeSeconds(*TrueTime).ToString());
            //PluginLog.Information($"{DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Hour:00}:{DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Minute:00}:{DateTimeOffset.FromUnixTimeSeconds(*TrueTime).Second:00}");

            //PluginLog.Information($"{calcHours.ToString()} - {Climates.GetTemperature(this.clientState.TerritoryType, calcHours).ToString()}");

            //if (Configuration.enableRainPopup...
            if (!Plugin.Singleton.IsPlayerOccupied())
            {
                if (*currentWeather != prevWeather)
                {
                    if (rainWeathers.Contains(*currentWeather) && !rainWeathers.Contains(prevWeather) && !Plugin.Condition[ConditionFlag.UsingParasol])
                    {
                        isRaining = true;
                        ChatHelper.Echo("Gentle raindrops begin to fall upon your skin.", Plugin.Configuration.temperatureChatType, "Weather");
                        //WindowSystem.GetWindow("No Rain Prompt").IsOpen = false;
                        //WindowSystem.GetWindow("Rain Prompt").IsOpen = true;
                    }
                    else if (!rainWeathers.Contains(*currentWeather) && rainWeathers.Contains(prevWeather))
                    {
                        isRaining = false;
                        ChatHelper.Echo("The rain begins to clear.", Plugin.Configuration.temperatureChatType, "Weather");
                        /*WindowSystem.GetWindow("Rain Prompt").IsOpen = false;
                        if (Condition[ConditionFlag.UsingParasol])
                        {
                            WindowSystem.GetWindow("No Rain Prompt").IsOpen = true;
                        }*/
                    }

                    prevWeather = *currentWeather;
                }
            }
        }
    }
}
