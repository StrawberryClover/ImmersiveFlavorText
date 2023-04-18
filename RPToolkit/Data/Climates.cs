using Dalamud.Logging;
using RPToolkit.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Data
{
    internal class Climates
    {
        public delegate string nameTest(string name);
        public static SortedDictionary<int, TemperatureDescription> temperatureStages { get; private set; } = new SortedDictionary<int, TemperatureDescription>
        {
            // Temperature, Stage Increase Description, Stage Decrease Description
            {100, Localization.loadedLocalization.temperatureStages.heatwave},
            {90, Localization.loadedLocalization.temperatureStages.veryHot },
            {76, Localization.loadedLocalization.temperatureStages.hot },
            {68, Localization.loadedLocalization.temperatureStages.roomTemp },
            {60, Localization.loadedLocalization.temperatureStages.mild },
            {50, Localization.loadedLocalization.temperatureStages.lukewarm },
            {40, Localization.loadedLocalization.temperatureStages.chilled },
            {25, Localization.loadedLocalization.temperatureStages.cold },
            {10, Localization.loadedLocalization.temperatureStages.veryCold },
            {0, Localization.loadedLocalization.temperatureStages.frigid }
        };

        public static float hottestHour = 15.5f;
        public static float coldestHour = 5.5f;

        #region Biome Temperature Variables
        public static Temperature indoors = new Temperature(68, 76);
        public static Temperature desert = new Temperature(66, 100); //25 at night? idk lmao
        public static Temperature warmCoastal = new Temperature(68, 86);
        public static Temperature coldCoastal = new Temperature(49, 66);
        public static Temperature shrubland = new Temperature(55, 79);
        public static Temperature tundra = new Temperature(2, 27);
        public static Temperature temperateForest = new Temperature(53, 85);
        public static Temperature rainForest = new Temperature(75, 91);
        public static Temperature highAltitude = new Temperature(18, 50); //based on altitude of 3.3k yalms (around 3k meters higher than I measured using Sohm Al, which seemed to be about 800) and using Temperate Forest as average temp
        public static Temperature volcanic = new Temperature(100, 125);
        public static Temperature crystal = new Temperature(30, 55);
        public static Temperature oceanFloor = new Temperature(30, 38);
        public static Temperature subZero = new Temperature(-66, -81);
        public static Temperature mountains = new Temperature(29, 53);
        #endregion

        public struct Temperature
        {
            public int low;
            public int high;

            public Temperature(int low, int high)
            {
                this.low = low;
                this.high = high;
            }
        }

        public struct ZoneTemperature
        {
            public Temperature baseTemperature;
            public Dictionary<uint, Temperature> subAreas = new Dictionary<uint, Temperature>();

            public ZoneTemperature(Temperature baseTemperature, Dictionary<uint, Temperature>? subAreas = null)
            {
                this.baseTemperature = baseTemperature;
                if (subAreas != null) this.subAreas = subAreas;
            }
        }

        /// <summary>
        /// High and Low temperatures by zone.
        /// </summary>
        /// <value>
        /// <br>Key: Zone ID</br>
        /// <br>Value.low: Low Temperature</br>
        /// <br>Value.high: High Temperature</br>
        /// </value>
        public static Dictionary<ushort, ZoneTemperature> zoneTemperatures { get; private set; } = new Dictionary<ushort, ZoneTemperature>
        {
            // Yes, in the end I've decided to use Fahrenheit. There are 2 reasons for this:
            // 1. I live in the US, unfortunately, and since I am the main person coming up with these temperature values, it's easier for me to understand since it's what everyone around me uses irl and that I get told on a day to day basis.
            // 2. I just felt like it might be easier to use ints instead of floats, and might be a smaller data type, even though that doesn't really make much of a difference.
            //
            // Zone ID, Avg High Temp, Avg Low Temp
            {128, new ZoneTemperature(warmCoastal)}, //Limsa (Upper)
            {129, new ZoneTemperature(warmCoastal)}, //Limsa (Lower)
            {130, new ZoneTemperature(desert,new Dictionary<uint, Temperature>() //Ul dah - Steps of Nald
            {
                {615, indoors} //Quicksand
            })},
            {131, new ZoneTemperature(desert)}, //Ul dah - Steps of Thal
            {132, new ZoneTemperature(temperateForest)}, //New Gridania
            {133, new ZoneTemperature(temperateForest)}, //Old Gridania
            {134, new ZoneTemperature(warmCoastal)}, //Middle La Noscea
            {135, new ZoneTemperature(warmCoastal)}, //Lower La Noscea
            {137, new ZoneTemperature(warmCoastal)}, //Eastern La Noscea
            {138, new ZoneTemperature(warmCoastal)}, //Western La Noscea
            {139, new ZoneTemperature(warmCoastal)}, //Upper La Noscea
            {140, new ZoneTemperature(desert, new Dictionary<uint, Temperature>() //Western Thanalan
            {
                {246, warmCoastal }, //Cape Westwind
                {274, warmCoastal }, //Vesper Bay
                {472, indoors } //Waking Sands
            })},
            {141, new ZoneTemperature(desert)}, //Central Thanalan
            {144, new ZoneTemperature(indoors)}, //Gold Saucer
            {145, new ZoneTemperature(desert)}, //Eastern Thanalan
            {146, new ZoneTemperature(desert)}, //Southern Thanalan
            {147, new ZoneTemperature(desert)}, //Northern Thanalan
            {148, new ZoneTemperature(temperateForest)}, //Central Shroud
            {150, new ZoneTemperature(crystal)}, //[Dungeon]KeepersOfTheLake (Old?)
            {152, new ZoneTemperature(temperateForest)}, //East Shroud
            {153, new ZoneTemperature(temperateForest)}, //South Shroud
            {154, new ZoneTemperature(temperateForest)}, //North Shroud
            {155, new ZoneTemperature(tundra)}, //Coerthas
            {156, new ZoneTemperature(crystal)}, //Mor Dhona
            {157, new ZoneTemperature(new Temperature(77, 77))}, //[Dungeon]Sastasha (Old?)
            {158, new ZoneTemperature(warmCoastal)}, //[Dungeon]Brayflox (Old?)
            {159, new ZoneTemperature(warmCoastal, new Dictionary<uint, Temperature>() //[Dungeon]WanderersPalace
            {
                {790, indoors} //Long Hall
            })},
            {160, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]PharosSirius
            {161, new ZoneTemperature(new Temperature(83, 83))}, //[Dungeon]Copperbell
            {162, new ZoneTemperature(indoors)}, //[Dungeon]Halatali
            {163, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]SunkenTemple
            {164, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]TamTara
            {166, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]Haukke
            {167, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]AmdaporKeep
            {168, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]StoneVigil
            {169, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]TotoRak
            {170, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]CuttersCry
            {171, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]DzemaelDarkhold
            {172, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]AurumVale
            {175, new ZoneTemperature(warmCoastal)}, //Wolves Den
            {177, new ZoneTemperature(indoors)}, //Limsa Lominsa - Inn
            {178, new ZoneTemperature(indoors)}, //Ul dah - Inn
            {179, new ZoneTemperature(indoors)}, //Gridania - Inn
            {180, new ZoneTemperature(warmCoastal)}, //Outer La Noscea
            {181, new ZoneTemperature(warmCoastal)}, //Limsa (Upper)
            {182, new ZoneTemperature(desert)}, //Ul dah - Steps of Nald
            {183, new ZoneTemperature(shrubland)}, //New Gridania
            {186, new ZoneTemperature(warmCoastal)}, //Wolves Den
            {198, new ZoneTemperature(warmCoastal)}, //Limsa Lominsa - Command
            {202, new ZoneTemperature(volcanic)}, //[Trial]Ifrit
            {204, new ZoneTemperature(indoors)}, //Gridania - First Bow
            {205, new ZoneTemperature(temperateForest)}, //Gridania - Lotus Stand
            {206, new ZoneTemperature(new Temperature(0, 0))}, //[Trial]Titan
            {207, new ZoneTemperature(temperateForest)}, //[Trial]MoogleMog
            {208, new ZoneTemperature(new Temperature(0, 0))}, //[Trial]Garuda
            {210, new ZoneTemperature(desert)}, //Ul dah - Heart of the Sworn
            {212, new ZoneTemperature(indoors)}, //Waking Sands
            {250, new ZoneTemperature(warmCoastal)}, //Wolves Den Pier
            {266, new ZoneTemperature(desert)}, //[Instanced]Eastern Thanalan (BLM Job Quest)
            {269, new ZoneTemperature(desert)}, //[Instanced]Moondrip
            {281, new ZoneTemperature(new Temperature(0, 0))}, //[Trial]Leviathan
            {286, new ZoneTemperature(new Temperature(0, 0))}, //ImOnABoat
            {288, new ZoneTemperature(new Temperature(0, 0))}, //ImOnABoat
            {292, new ZoneTemperature(volcanic)}, //[Trial]Bowl of Embers
            {294, new ZoneTemperature(highAltitude)}, //[Trial]Howling Eye
            {297, new ZoneTemperature(highAltitude)}, //[Trial]Howling Eye (Extreme)
            {331, new ZoneTemperature(new Temperature(0, 0))}, //Garuda_Entrance
            {332, new ZoneTemperature(warmCoastal)}, //[Trial]CapeWestwind
            {336, new ZoneTemperature(warmCoastal)}, //Wolves Den
            {337, new ZoneTemperature(warmCoastal)}, //Wolves Den
            {339, new ZoneTemperature(warmCoastal)}, //Mist
            {340, new ZoneTemperature(temperateForest)}, //Lavender Beds
            {341, new ZoneTemperature(new Temperature(60, 91))}, //The Goblet
            {345, new ZoneTemperature(indoors)}, //Private Cottage - The Goblet
            {349, new ZoneTemperature(new Temperature(83, 83))}, //[Dungeon]CopperbellHM
            {350, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]HaukkeHM
            {351, new ZoneTemperature(indoors)}, //Rising Stones
            {352, new ZoneTemperature(warmCoastal)}, //Wolves Den
            {359, new ZoneTemperature(warmCoastal)}, //[Trial]The Whorleater
            {360, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]HalataliHM
            {361, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]HullbreakerIsle
            {362, new ZoneTemperature(warmCoastal)}, //[Dungeon]BrayfloxHM
            {363, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]LostCity
            {364, new ZoneTemperature(temperateForest)}, //[Trial]Thornmarch
            {365, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]StoneVigilHM
            {367, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]SunkenTempleHM
            {371, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]Snowcloak
            {373, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]TamTaraHM
            {374, new ZoneTemperature(new Temperature(0, 0))}, //[Trial]Ramuh
            {376, new ZoneTemperature(new Temperature(0, 0))}, //Frontlines
            {377, new ZoneTemperature(new Temperature(0, 0))}, //[Trial]Shiva
            {386, new ZoneTemperature(indoors)}, //Goblet Private Quarters
            {387, new ZoneTemperature(new Temperature(77, 77))}, //[Dungeon]SastashaHM
            {388, new ZoneTemperature(indoors)}, //Gold Saucer - Chocobo Square
            {395, new ZoneTemperature(new Temperature(0, 0))}, //Intercessory
            {397, new ZoneTemperature(tundra)}, //Coerthas Western Highlands
            {398, new ZoneTemperature(new Temperature(28, 66))}, //The Dravanian Forelands
            {399, new ZoneTemperature(highAltitude)}, //The Dravanian Hinterlands
            {400, new ZoneTemperature(highAltitude)}, //The Churning Mists
            {401, new ZoneTemperature(highAltitude)}, //Sea of Clouds
            {402, new ZoneTemperature(highAltitude)}, //Azys Lla
            {418, new ZoneTemperature(tundra)}, //Ishgard - Foundation
            {419, new ZoneTemperature(tundra)}, //Ishgard - The Pillars
            {421, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]Vault
            {422, new ZoneTemperature(new Temperature(0, 0))}, //Frontlines - Slaughter
            {424, new ZoneTemperature(indoors)}, //Company Workshop (The Goblet)
            {426, new ZoneTemperature(new Temperature(0, 0))}, //[Trial]Chrysalis
            {427, new ZoneTemperature(indoors)}, //Ishgard - Scholasticate
            {433, new ZoneTemperature(indoors)}, //Ishgard - Fortempts Manor
            {434, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]DuskVigil
            {435, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]Aery
            {436, new ZoneTemperature(new Temperature(18, 28))}, //[Trial]The Limitless Blue
            {439, new ZoneTemperature(new Temperature(0, 0))}, //Ishgard - Chocobo Proving Grounds
            {441, new ZoneTemperature(new Temperature(0, 0))}, //[Dungeon]SohmAl
            {447, new ZoneTemperature(new Temperature(18, 28))}, //[Trial]The Limitless Blue (Extreme)
            {456, new ZoneTemperature(new Temperature(0, 0))}, //Ishgard - Ruling Chamber
            {463, new ZoneTemperature(new Temperature(0, 0))}, //Matoyas Cave
            {478, new ZoneTemperature(new Temperature(0, 0))}, //Idyllshire
            {554, new ZoneTemperature(new Temperature(0, 0))}, //[PVP] - Fields of Glory (Shatter)
            {612, new ZoneTemperature(desert)}, //The Fringes
            {613, new ZoneTemperature(warmCoastal, new Dictionary<uint, Temperature>() //The Ruby Sea
            {
                {2762, volcanic}, //Hells' Lid
            }) },
            {614, new ZoneTemperature(temperateForest)}, //Yanxia
            {620, new ZoneTemperature(desert)}, //The Peaks
            {621, new ZoneTemperature(desert)}, //The Lochs
            {622, new ZoneTemperature(mountains)}, //The Azim Steppe
            {626, new ZoneTemperature(coldCoastal)}, //[Dungeon]The Sirensong Sea
            {628, new ZoneTemperature(coldCoastal)}, //Kugane
            {635, new ZoneTemperature(desert)}, //Rhalgrs Reach
            {639, new ZoneTemperature(new Temperature(0, 0))}, //Ruby Bazaar Offices
            {674, new ZoneTemperature(coldCoastal)}, //(Trial)The Pool of Tribute (Extreme)
            {680, new ZoneTemperature(new Temperature(0, 0))}, //ImOnABoatAgain
            {681, new ZoneTemperature(new Temperature(0, 0))}, //The House of the Fierce
            {683, new ZoneTemperature(new Temperature(0, 0))}, //First Alter of Djanan
            {719, new ZoneTemperature(desert)}, //[Trial]Emanation
            {720, new ZoneTemperature(desert)}, //[Trial]Emanation (Extreme)
            {730, new ZoneTemperature(highAltitude)}, //[Trial]The Pool of Tribute (Extreme)
            {732, new ZoneTemperature(temperateForest)}, //Eureka Anemos
            {759, new ZoneTemperature(coldCoastal)}, //Doman Enclave
            {763, new ZoneTemperature(tundra)}, //Eureka Pagos
            {779, new ZoneTemperature(indoors)}, //[Dungeon]Castrum Fluminis
            {796, new ZoneTemperature(indoors)}, //Masked Carnival (Ul'dah)
            {813, new ZoneTemperature(temperateForest)}, //Lakeland
            {814, new ZoneTemperature(coldCoastal)}, //Kholusia
            {815, new ZoneTemperature(desert)}, //Amh Araeng
            {816, new ZoneTemperature(shrubland)}, //Il Mheg
            {817, new ZoneTemperature(rainForest)}, //Rak'tika Greatwood
            {818, new ZoneTemperature(oceanFloor)}, //The Tempest
            {819, new ZoneTemperature(temperateForest)}, //The Crystarium
            {820, new ZoneTemperature(indoors, new Dictionary<uint, Temperature>() //Eulmore
            {
                {3258, coldCoastal}, //Derelicts
                {3279, coldCoastal}, //Skyfront
            }) },
            {845, new ZoneTemperature(indoors)}, //[Trial]The Dancing Plague
            {849, new ZoneTemperature(indoors)}, //[Raid]Eden's Gate: Resurrection
            {850, new ZoneTemperature(highAltitude)}, //[Raid]Eden's Gate: Descent
            {851, new ZoneTemperature(oceanFloor)}, //[Raid]Eden's Gate: Inundation
            {852, new ZoneTemperature(highAltitude)}, //[Raid]Eden's Gate: Supulture
            {858, new ZoneTemperature(indoors)}, //[Trial]The Dancing Plague (Extreme)
            {884, new ZoneTemperature(indoors)}, //[Dungeon]The Grand Cosmos
            {898, new ZoneTemperature(oceanFloor, new Dictionary<uint, Temperature>() //[Dungeon]Anamnesis Anyder
            {
                {3461, indoors} //Anamnesis
            })},
            {902, new ZoneTemperature(shrubland)}, //[Raid]Eden's Verse: Fulmination
            {903, new ZoneTemperature(highAltitude)}, //[Raid]Eden's Verse: Furor
            {904, new ZoneTemperature(highAltitude)}, //[Raid]Eden's Verse: Iconoclasm
            {905, new ZoneTemperature(subZero)}, //[Raid]Eden's Verse: Refulgence
            {934, new ZoneTemperature(indoors)}, //[Trial]Castrum Marinum/Emerald Weapon
            {957, new ZoneTemperature(rainForest)}, //Thavnair
            {956, new ZoneTemperature(temperateForest)}, //Labyrinthos
            {958, new ZoneTemperature(tundra)}, //Garlemald
            {959, new ZoneTemperature(new Temperature(63, 63))}, //Mare Lamentorum
            {961, new ZoneTemperature(temperateForest)}, //Elpis
            {962, new ZoneTemperature(coldCoastal)}, //Old Sharlayan
            {963, new ZoneTemperature(rainForest, new Dictionary<uint, Temperature>() //Radz-at-Han
            {
                {3868, indoors}, //Ruveydah Fibers
                {3869, indoors} //Artha
            })},
            {979, new ZoneTemperature(tundra)}, //Empyreum
            {1025, new ZoneTemperature(crystal)}, //[Instance]Gates of Pandaemonium
            {1036, new ZoneTemperature(new Temperature(77, 77))}, //[Dungeon]Sastasha (New?)
            {1038, new ZoneTemperature(new Temperature(83, 83))}, //[Dungeon]Copperbell (New)
            {1041, new ZoneTemperature(warmCoastal)}, //[Dungeon]Brayflox (New?)
            {1043, new ZoneTemperature(new Temperature(crystal.low, crystal.low))}, //[Dungeon]Castrum Meridianum
            {1045, new ZoneTemperature(volcanic)}, //[Trial]Bowl of Embers (New?)
            {1050, new ZoneTemperature(indoors, new Dictionary<uint, Temperature>() //[Dungeon]Alzadaal's Legacy
            {
                {4161, rainForest}, //Bhaflau Remnants
            }) },
            {1056, new ZoneTemperature(indoors)}, //[Dungeon]Alzadaal's Legacy (MSQ Instance)
            {1057, new ZoneTemperature(indoors)}, //Restricted Archives (MSQ Instance)
            {1063, new ZoneTemperature(crystal)}, //[Dungeon]KeepersOfTheLake (New?)
            {1066, new ZoneTemperature(indoors)}, //[Dungeon]The Vault
            {1067, new ZoneTemperature(temperateForest)}, //[Trial]Thornmarch (New?)
            {1070, new ZoneTemperature(indoors, new Dictionary<uint, Temperature>() //[Dungeon]The Fell Court of Troia
            {
                {4190, crystal}, //Hydromantic Terraces
            })},
            {1071, new ZoneTemperature(crystal)}, //[Trial]Storm's Crown
            {1073, new ZoneTemperature(shrubland)}, //Elysion
            {1077, new ZoneTemperature(crystal)}, //[Instanced]Zero's Domain
            {1078, new ZoneTemperature(indoors)}, //[Instanced]Meghaduta Guest Chambers
            {1081, new ZoneTemperature(crystal)}, //[Raid]Abyssos: The Fifth Circle
            {1083, new ZoneTemperature(indoors)}, //[Raid]Abyssos: The Sixth Circle
            {1085, new ZoneTemperature(indoors)}, //[Raid]Abyssos: The Seventh Circle
            {1087, new ZoneTemperature(indoors)}, //[Raid]Abyssos: The Eighth Circle
            {1089, new ZoneTemperature(indoors, new Dictionary<uint, Temperature>() //[Instanced]The Fell Court of Troia
            {
                {4190, crystal}, //Hydromantic Terraces
            })},
            {1091, new ZoneTemperature(indoors, new Dictionary<uint, Temperature>() //[Instanced]The Fell Court of Troia
            {
                {4190, crystal}, //Hydromantic Terraces
            })},
            {1092, new ZoneTemperature(crystal)}, //[Instanced]Storm's Crown
            {1093, new ZoneTemperature(indoors)}, //[Instance]Absyssos: The Eighth Circle
        };

        /// <summary>
        /// How much weather affects the temperature.
        /// </summary>
        /// <value>
        /// <br>Key: Weather ID</br>
        /// <br>Value: Temperature Offset</br>
        /// </value>
        public static Dictionary<int, int> weatherTemperatures { get; private set; } = new Dictionary<int, int>
        {
            {1, 0}, //Clear Skies
            {2, 0}, //Fair Skies
            {3, -4}, //Clouds
            {4, -8}, //Fog
            {5, -2}, //Wind
            {6, -4}, //Gales
            {7, -6}, //Rain
            {8, -8}, //Shower
            {9, -4}, //Thunder
            {10, -8}, //Thunderstorms
            {11, 1}, //Dust Storms
            {12, 2}, //Sandstorms
            {13, 6}, //Hot Spells
            {14, 10}, //Heat Waves
            {15, -6}, //Snows
            {16, -10}, //Blizzards
            {17, -3}, //Gloom
            {26, 10}, //Heat Waves
            {28, -4}, //Gales
        };

        public static int GetTemperature(ushort zoneId, uint areaId, uint subAreaId, float time)
        {
            float perc;
            if (time < coldestHour)
            {
                perc = 1 - (time + 24 - hottestHour) / (coldestHour + 24 - hottestHour);
            }
            else if (time > hottestHour)
            {
                perc = 1 - (time - hottestHour) / (24 + coldestHour - hottestHour);
            }
            else
            {
                perc = (time - coldestHour) / (hottestHour - coldestHour);
            }
            if (zoneTemperatures.ContainsKey(zoneId))
            {
                var baseZone = zoneTemperatures[zoneId];
                var refTemp = baseZone.baseTemperature;

                if (subAreaId != 0 && baseZone.subAreas.ContainsKey(subAreaId))
                    refTemp = baseZone.subAreas[subAreaId];
                else if (areaId != 0 && baseZone.subAreas.ContainsKey(areaId))
                    refTemp = baseZone.subAreas[areaId];

                //int temp = (int)Math.Round(perc * (temperatures[zoneId].high - temperatures[zoneId].low) + temperatures[zoneId].low);
                //PluginLog.Information($"{perc}: {SinDistribution(perc, 0.5, 2.0, temperatures[zoneId].low, temperatures[zoneId].high)}");
                var temp = (int)Math.Round(SinDistribution(perc, 0.5, 2.0, refTemp.low, refTemp.high));
                //PluginLog.Information($"{perc + ((perc - 0.5) * 0.5)}");

                return temp;
            }
            else return 0;
        }

        private static double SinDistribution(double value, double lowToHighMeanPoint, double length, double low, double high)
        {
            var amplitude = (high - low) / 2;
            var mean = low + amplitude;
            //PluginLog.Information($"({value} - {lowToHighMeanPoint}) % {length}) = {((value - lowToHighMeanPoint) % length).ToString()}");
            return mean + amplitude * Math.Sin(
                (value - lowToHighMeanPoint) % length / length * 2 * Math.PI);
        }
    }
}
