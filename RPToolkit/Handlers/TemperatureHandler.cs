using Dalamud.Game.ClientState.Conditions;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Plugin;
using RPToolkit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Handlers
{
    internal unsafe class TemperatureHandler
    {
        public static int currentTemperatureStage = 0;
        public static int currentTemp { get; private set; }
        private static int secPassed = 0;
        private static int secUntilDivergenceUpdate = 300;
        private static int temperatureStageShiftSec = 0;
        private static int temperatureStageShiftCooldown = 120;
        public static int temperatureDivergence = 0;
        private static int temperatureDivergenceLimit = 5;

        private static int currentZone;

        public static void UpdateTemps(Object? source, System.Timers.ElapsedEventArgs e)
        {
            UpdateTemps();
        }

        public static void UpdateTemps()
        {
            if (!Plugin.Condition[ConditionFlag.BetweenAreas] && !Plugin.Condition[ConditionFlag.BetweenAreas51])
            {
                if (Plugin.Configuration.enableTemperatureMessages && Climates.zoneTemperatures.ContainsKey(Plugin.Singleton.clientState.TerritoryType) && (Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].low != 0 && Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].high != 0))
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

                    int hours = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Hour;
                    int minutes = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Minute;
                    int seconds = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Second;
                    float calcHours = hours + ((float)minutes / 60) + ((float)seconds / 60 / 60);
                    currentTemp =
                        Climates.GetTemperature(Plugin.Singleton.clientState.TerritoryType, calcHours) +
                        (Climates.weatherTemperatures.ContainsKey(*WeatherHandler.currentWeather) ?
                            Climates.weatherTemperatures[*WeatherHandler.currentWeather]
                        :
                            Climates.weatherTemperatures[0]
                        ) +
                        temperatureDivergence;

                    if (Plugin.Singleton.PluginInterface.IsDev)
                        PluginLog.Information($"({DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Hour.ToString().PadLeft(2, '0')}:{DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Minute.ToString().PadLeft(2, '0')}:{DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Second.ToString().PadLeft(2,'0')} ET) Current Temp: {currentTemp.ToString()} [Base: {Climates.GetTemperature(Plugin.Singleton.clientState.TerritoryType, calcHours)}, Divergence: {temperatureDivergence}, Weather: {(Climates.weatherTemperatures.ContainsKey(*WeatherHandler.currentWeather) ? Climates.weatherTemperatures[*WeatherHandler.currentWeather] : Climates.weatherTemperatures[0])}]");

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
                    temperatureStageShiftSec++;
                    if (currentTemperatureStage != newStage && temperatureStageShiftSec >= temperatureStageShiftCooldown)
                    {
                        if (newStage < currentTemperatureStage)
                            ChatHelper.Echo(Climates.temperatureStages[newStage].decreaseDesc, Plugin.Configuration.temperatureChatType, "Temperature");
                        else if (newStage > currentTemperatureStage)
                            ChatHelper.Echo(Climates.temperatureStages[newStage].increaseDesc, Plugin.Configuration.temperatureChatType, "Temperature");
                        currentTemperatureStage = newStage;
                        temperatureStageShiftSec = 0;
                    }
                }
                else
                {
                    if (currentZone != Plugin.Singleton.clientState.TerritoryType)
                        if (Plugin.Configuration.showTemperatureSuggestionPopup)
                            Plugin.DrawWindow("Temperature Suggestion Window");
                        else
                            ChatHelper.SendSuggestionMessage();
                }
                currentZone = Plugin.Singleton.clientState.TerritoryType;
            }
        }
    }
}
