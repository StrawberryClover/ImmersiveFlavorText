using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Common.Component.BGCollision;
using FFXIVClientStructs.FFXIV.Common.Math;
using Lumina.Excel.GeneratedSheets;
using RPToolkit.Data;
using RPToolkit.Helpers;
using RPToolkit.Localizations;
using RPToolkit.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object = System.Object;

namespace RPToolkit.Handlers
{
    internal unsafe class TemperatureHandler
    {
        public static int currentTemperatureStage = 0;
        public static int currentTemp { get; private set; }
        private static int maxTempChangePerSecond = 1;
        private static int secPassed = 0;
        private static int secUntilDivergenceUpdate = 300;
        private static int temperatureStageShiftSec = 0;
        private static int temperatureStageShiftCooldown = 120;
        public static int temperatureDivergence = 0;
        private static int temperatureDivergenceLimit = 5;
        public static float consumableAdjustment = 0f;
        private static float consumableDecay = 0.1f;
        public static float maxConsumableAdjustment { get; private set; } = 12f;

        private static int currentZone;
        public static string debugInfo;

        public static RaycastHit? lastShadedHit = null;

        public static void UpdateTemps(Object? source, System.Timers.ElapsedEventArgs e)
        {
            UpdateTemps();
        }

        public static void UpdateTemps()
        {
            if (!Plugin.Singleton.IsPlayerOccupied())
            {
                if (Plugin.Configuration.showTemperatureMessages && Climates.zoneTemperatures.ContainsKey(Plugin.Singleton.clientState.TerritoryType) && (Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.low != 0 && Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.high != 0))
                {
                    var climate = Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType];


                    // Random Temperature Variance
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

                    // Base Temperature
                    float calcHours = TimeHelper.GetTotalHours();
                    int newTemp = Climates.GetTemperature(Plugin.Singleton.clientState.TerritoryType, Plugin.AreaInfo->AreaPlaceNameID, Plugin.AreaInfo->SubAreaPlaceNameID, calcHours);
                    newTemp += (Climates.weatherTemperatures.ContainsKey(*WeatherHandler.currentWeather)
                        ? Climates.weatherTemperatures[*WeatherHandler.currentWeather]
                        : 0);
                    newTemp += temperatureDivergence;

                    //Altitude Adjustments
                    float adjustedAltitude = climate.metersAboveSeaLevel;
                    if (Plugin.Singleton.clientState.LocalPlayer != null)
                    {
                        adjustedAltitude += Plugin.Singleton.clientState.LocalPlayer.Position.Y;
                    }
                    int altitudeTempDifference = -(CelsiusToFahrenheit(adjustedAltitude * 0.00650f) - 32);
                    newTemp += altitudeTempDifference;


                    // Shade Adjustments
                    int shadeAdjustment = 0;
                    if (Plugin.Configuration.enableShade && (climate.baseTemperature.high != Climates.indoors.high && climate.baseTemperature.low != Climates.indoors.low) && Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.low != Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.high)
                    {
                        bool inShade = RaycastSun();
                        if (inShade && newTemp >= 70)
                        {
                            float shadePerc = (newTemp - 70) / 40f;
                            if (shadePerc > 1) shadePerc = 1;
                            shadeAdjustment = (int)(shadePerc * -15f);
                        }
                    }
                    newTemp += shadeAdjustment;

                    // Player Wetness Adjustments (swimming, raining)
                    if (Plugin.Condition[ConditionFlag.Swimming] || Plugin.Condition[ConditionFlag.Diving])
                    {
                        newTemp -= 11;
                    }
                    else if (WeatherHandler.isRaining && !Plugin.Condition[ConditionFlag.UsingParasol])
                    {
                        newTemp -= 6;
                    }

                    // Food/Water adjustments
                    if (Math.Abs(consumableAdjustment) <= consumableDecay) consumableAdjustment = 0;
                    else if (consumableAdjustment > 0) consumableAdjustment -= consumableDecay;
                    else if (consumableAdjustment < 0) consumableAdjustment += consumableDecay;
                    newTemp += (int)Math.Round(consumableAdjustment);

                    // Smooth temperature shift
                    if (currentZone != 0 && Math.Abs(newTemp - currentTemp) > maxTempChangePerSecond)
                        currentTemp += newTemp < currentTemp ? -maxTempChangePerSecond : maxTempChangePerSecond;
                    else currentTemp = newTemp;

                    // Debug Message
                    if (Plugin.Singleton.PluginInterface.IsDev)
                        debugInfo = 
                            //$"({TimeHelper.GetHours().ToString().PadLeft(2, '0')}:{TimeHelper.GetMinutes().ToString().PadLeft(2, '0')}:{TimeHelper.GetSeconds().ToString().PadLeft(2, '0')} ET) " +
                            $"Current Temperature: {ConvertTemperature(currentTemp)} " +
                            $"\r\n    Base: {ConvertTemperature(Climates.GetTemperature(Plugin.Singleton.clientState.TerritoryType, Plugin.AreaInfo->AreaPlaceNameID, Plugin.AreaInfo->SubAreaPlaceNameID, calcHours))} " +
                            $"\r\n    Variance: {ConvertTemperature(temperatureDivergence)}  " +
                            $"\r\n    Weather Modifier: {ConvertTemperature((Climates.weatherTemperatures.ContainsKey(*WeatherHandler.currentWeather) ? Climates.weatherTemperatures[*WeatherHandler.currentWeather] : 0))} " +
                            $"\r\n    Shade: {ConvertTemperature(shadeAdjustment)} " +
                            $"\r\n    Wetness: {ConvertTemperature(((Plugin.Condition[ConditionFlag.Swimming] || Plugin.Condition[ConditionFlag.Diving]) ? -11 : WeatherHandler.isRaining ? -6 : 0))}" +
                            $"\r\n    Food/Drink: {ConvertTemperature((int)Math.Round(consumableAdjustment))}" +
                            $"\r\n    Altitude: {ConvertTemperature(altitudeTempDifference)} ({(adjustedAltitude > -1 && adjustedAltitude < 0 ? "0" : Math.Round(adjustedAltitude))} yalms above sea level)";

                    // Temperature Stage
                    int newStage = currentTemperatureStage;
                    foreach (KeyValuePair<int, TemperatureDescription> tempStages in Climates.temperatureStages)
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
                    // Limit Stage Shift Frequency
                    temperatureStageShiftSec++;
                    if (currentTemperatureStage != newStage && temperatureStageShiftSec >= temperatureStageShiftCooldown)
                    {
                        if (newStage < currentTemperatureStage)
                            ChatHelper.Echo(Climates.temperatureStages[newStage].decreaseDesc, Plugin.Configuration.flavorTextChatType, "Temperature");
                        else if (newStage > currentTemperatureStage)
                            ChatHelper.Echo(Climates.temperatureStages[newStage].increaseDesc, Plugin.Configuration.flavorTextChatType, "Temperature");
                        currentTemperatureStage = newStage;
                        temperatureStageShiftSec = 0;
                    }
                }
                else
                {
                    if (currentZone != Plugin.Singleton.clientState.TerritoryType)
                        if (Plugin.Configuration.showTemperatureSuggestionPopup)
                            TempSuggestionWindow.window.IsOpen = true;
                        else
                            ChatHelper.SendSuggestionMessage();

                    debugInfo = "";
                }
                currentZone = Plugin.Singleton.clientState.TerritoryType;
            }
        }

        public static void NewZone()
        {
            if (Climates.zoneTemperatures.ContainsKey(Plugin.Singleton.clientState.TerritoryType) && (Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.low != 0 && Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.high != 0))
            {
                if (currentTemp < Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.low)
                    currentTemp = Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.low;
                else if (currentTemp > Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.high)
                    currentTemp = Climates.zoneTemperatures[Plugin.Singleton.clientState.TerritoryType].baseTemperature.high;
            }
        }


        private static bool RaycastSun()
        {
            if (Plugin.Singleton.clientState.LocalPlayer?.Position == null) return false;
            Vector3[] sunriseToSunsetCurve =
            {
                new Vector3(1.375f, 1f, 0.25f),
                new Vector3(0.46f, 1f, 0.325f),
                new Vector3(0f, 1f, 0.5f),
                new Vector3(-0.46f, 1f, 0.325f),
                new Vector3(-1.375f, 1f, 0.25f)
            };
            bool hitSomething = false;
            float hour = TimeHelper.GetTotalHours();
            if (hour > 6 && hour < 18) // Day Time
            {
                float sunriseHour = 6;
                float sunsetHour = 18;
                float hourDifference = sunsetHour - sunriseHour;
                float hoursAfterSunrise = hour - sunriseHour;
                float totalPerc = hoursAfterSunrise / hourDifference;
                float hoursPerStage = hourDifference / (sunriseToSunsetCurve.Length - 1);
                int stage = (int)MathF.Floor(hoursAfterSunrise / hoursPerStage);
                float adjustedPerc = hoursAfterSunrise % hoursPerStage / hoursPerStage;


                float distance = Vector3.Distance(sunriseToSunsetCurve[stage], sunriseToSunsetCurve[stage + 1]);
                float scaledDistance = distance * adjustedPerc;
                var difference = (sunriseToSunsetCurve[stage + 1] - sunriseToSunsetCurve[stage]).Normalized * scaledDistance;
                var direction = (sunriseToSunsetCurve[stage] + difference);

                //PluginLog.Information($"Stage: {stage}, Perc: {adjustedPerc}, V3A: {sunriseToSunsetCurve[stage]}, V3B: {sunriseToSunsetCurve[stage + 1]}, FinalV3: {direction}");

                //direction = lastQuarterDirection;
                RaycastHit hit;

                //var flags = stackalloc int[] { 0x2000, 0x0000, 0x4000, 0x0000 };
                // 1 << 0 = Invisible Ceiling?
                // 1 << 1 = ???
                // 1 << 2 = Objects, maybe terrain?
                // 1 << 3 = Some objects?
                // 1 << 4 = Invisible Ceiling?
                // 1 << 5 = ???
                // 1 << 6 = ???
                // 1 << 7 = ???
                // 1 << 8 = ???
                // 1 << 9 = ???
                // 1 << 10 = ???
                // 1 << 11 = ???
                // 1 << 12 = Objects in Gridania??
                // 1 << 13 = Invisible Walls + Everything else?
                // 1 << 14 = Objects, but not all objects, and maybe terrain?
                int flags = 0;
                //flags[2] |= 1 << 0;
                //flags[2] |= 1 << 4;
                flags |= 1 << 1;
                flags |= 1 << 2;
                flags |= 1 << 3;
                flags |= 1 << 12;
                //flags |= 1 << 13;
                flags |= 1 << 14;
                //flags |= 1 << 15;
                int mask = 1;


                BGCollisionModule* CollisionModule = FFXIVClientStructs.FFXIV.Client.System.Framework.Framework.Instance()->BGCollisionModule;
                hitSomething = CollisionModule->RaycastEx(&hit, (Vector3)Plugin.Singleton.clientState.LocalPlayer?.Position, direction, 10000, mask, &flags);
                if (hitSomething)
                {
                    lastShadedHit = hit;
                    //var hitObject = hit.Object;
                    //PluginLog.Information(hit.Object->GetType().ToString());
                }
            }
            if (!hitSomething) lastShadedHit = null;
            return hitSomething;
        }

        public static string GetTemperatureUnit()
        {
            if (!Plugin.Configuration.useCelsius.HasValue) return "";

            return Plugin.Configuration.useCelsius.Value ? "℃" : "°F";
        }

        public static string ConvertTemperature(int temperature)
        {
            if (!Plugin.Configuration.useCelsius.HasValue) return "";

            return Plugin.Configuration.useCelsius.Value ? $"{FahrenheitToCelsius(temperature)}℃" : $"{temperature}°F";
        }

        public static float FahrenheitToCelsius(int temperature)
        {
            return (temperature - 32) * 5 / 9;
        }

        public static int CelsiusToFahrenheit(float temperature)
        {
            return (int)MathF.Round(temperature * 1.8f) + 32;
        }
    }
}
