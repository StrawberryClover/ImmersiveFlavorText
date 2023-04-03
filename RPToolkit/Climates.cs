using Dalamud.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit
{
    internal class Climates
    {
        public struct TemperatureDescription
        {
            public string increaseDesc;
            public string decreaseDesc;

            public TemperatureDescription(string increaseDesc, string decreaseDesc)
            {
                this.increaseDesc = increaseDesc;
                this.decreaseDesc = decreaseDesc;
            }
        }
        public static SortedDictionary<int, TemperatureDescription> temperatureStages { get; private set; } = new SortedDictionary<int, TemperatureDescription>
        {
            // Temperature, Stage Increase Description, Stage Decrease Description
            {100, new TemperatureDescription("You don't know how much more of this heat you can take, it's unbearable, even in the shade.", "Nobody should see this message.") },
            {90, new TemperatureDescription("You start to feel a bit hotter now, very hot in fact. It would do you well to find shade soon.", "The heat begins to feel less unbearable, and shaded areas start to feel a lot nicer.") },
            {76, new TemperatureDescription("You begin to feel hot, some shade would be nice.", "You take a sigh of relief as things aren't as hot as they were before, but some shade would be nice.") },
            {68, new TemperatureDescription("It feels like the perfect temperature right now.", "It feels like the perfect temperature right now.") },
            {60, new TemperatureDescription("You begin to feel warm.", "The temperature seems to be cooling off a bit, things are starting to just feel warm now.") },
            {50, new TemperatureDescription("Things seem to be warming up a bit, now feeling lukewarm to you.", "It starts to feel lukewarm as the temperature cools down.") },
            {40, new TemperatureDescription("The cold is not as bad as before, you begin to feel just a bit chilled now.", "You start to feel chilly, things are starting to cool off quite a bit now.") },
            {25, new TemperatureDescription("You no longer feel so cold. You haven't stopped shivering, but it almost feels bearable now.", "You begin to feel quite cold, and start to shiver.") },
            {10, new TemperatureDescription("The air seems to lose it's frigid quality to it, but the ice cold bite in the air is not gone. It would be really nice to be near a campfire.", "You feel like you can't bear the cold anymore, your whole body is shivering uncontrollably. It would be really nice to warm yourself by a campfire right about now.") },
            {0, new TemperatureDescription("Nobody should see this message.", "The air feels absolutely frigid, at this rate you are going to freeze. All you can think about is sitting beside a warm campfire.") }
        };

        public static float hottestHour = 15.5f;
        public static float coldestHour = 5.5f;

        #region Biome Temperature Variables
        public static ZoneTemperature indoors = new ZoneTemperature(68, 76);
        public static ZoneTemperature desert = new ZoneTemperature(66, 100); //25 at night? idk lmao
        public static ZoneTemperature coastal = new ZoneTemperature(68, 86);
        public static ZoneTemperature shrubland = new ZoneTemperature(55, 79);
        public static ZoneTemperature tundra = new ZoneTemperature(2, 27);
        public static ZoneTemperature temperateForest = new ZoneTemperature(53, 85);
        public static ZoneTemperature rainForest = new ZoneTemperature(75, 88);
        #endregion

        public struct ZoneTemperature
        {
            public int low;
            public int high;

            public ZoneTemperature(int low, int high)
            {
                this.low = low;
                this.high = high;
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
            // Zone ID, Avg High Temp, Avg Low Temp (Yes, in the end I've decided to use Fahrenheit. I don't feel like using floats.)
            {128, coastal}, //Limsa (Upper)
            {129, coastal}, //Limsa (Lower)
            {130, desert}, //Ul dah - Steps of Nald
            {131, desert}, //Ul dah - Steps of Thal
            {132, temperateForest}, //New Gridania
            {133, temperateForest}, //Old Gridania
            {134, coastal}, //Middle La Noscea
            {135, coastal}, //Lower La Noscea
            {137, coastal}, //Eastern La Noscea
            {138, coastal}, //Western La Noscea
            {139, coastal}, //Upper La Noscea
            {140, desert}, //Western Thanalan
            {141, desert}, //Central Thanalan
            {144, indoors}, //Gold Saucer
            {145, desert}, //Eastern Thanalan
            {146, desert}, //Southern Thanalan
            {147, desert}, //Northern Thanalan
            {148, temperateForest}, //Central Shroud
            {150, new ZoneTemperature(30, 50)}, //[Dungeon]KeepersOfTheLake (Old?)
            {152, temperateForest}, //East Shroud
            {153, temperateForest}, //South Shroud
            {154, temperateForest}, //North Shroud
            {155, tundra}, //Coerthas
            {156, new ZoneTemperature(30, 50)}, //Mor Dhona
            {157, new ZoneTemperature(77, 77)}, //[Dungeon]Sastasha (Old?)
            {158, coastal}, //[Dungeon]Brayflox (Old?)
            {159, coastal}, //[Dungeon]WanderersPalace
            {160, new ZoneTemperature(0, 0)}, //[Dungeon]PharosSirius
            {161, new ZoneTemperature(0, 0)}, //[Dungeon]Copperbell
            {162, new ZoneTemperature(0, 0)}, //[Dungeon]Halatali
            {163, new ZoneTemperature(0, 0)}, //[Dungeon]SunkenTemple
            {164, new ZoneTemperature(0, 0)}, //[Dungeon]TamTara
            {166, new ZoneTemperature(0, 0)}, //[Dungeon]Haukke
            {167, new ZoneTemperature(0, 0)}, //[Dungeon]AmdaporKeep
            {168, new ZoneTemperature(0, 0)}, //[Dungeon]StoneVigil
            {169, new ZoneTemperature(0, 0)}, //[Dungeon]TotoRak
            {170, new ZoneTemperature(0, 0)}, //[Dungeon]CuttersCry
            {171, new ZoneTemperature(0, 0)}, //[Dungeon]DzemaelDarkhold
            {172, new ZoneTemperature(0, 0)}, //[Dungeon]AurumVale
            {175, coastal}, //Wolves Den
            {177, indoors}, //Limsa Lominsa - Inn
            {178, indoors}, //Ul dah - Inn
            {179, indoors}, //Gridania - Inn
            {180, coastal}, //Outer La Noscea
            {181, coastal}, //Limsa (Upper)
            {182, desert}, //Ul dah - Steps of Nald
            {183, shrubland}, //New Gridania
            {186, coastal}, //Wolves Den
            {198, coastal}, //Limsa Lominsa - Command
            {202, new ZoneTemperature(0, 0)}, //[Trial]Ifrit
            {204, new ZoneTemperature(0, 0)}, //Gridania - First Bow
            {205, new ZoneTemperature(0, 0)}, //Lotus Stand
            {206, new ZoneTemperature(0, 0)}, //[Trial]Titan
            {207, new ZoneTemperature(0, 0)}, //[Trial]MoogleMog
            {208, new ZoneTemperature(0, 0)}, //[Trial]Garuda
            {210, desert}, //Ul dah - Heart of the Sworn
            {212, indoors}, //Waking Sands
            {250, coastal}, //Wolves Den Pier
            {281, new ZoneTemperature(0, 0)}, //[Trial]Leviathan
            {286, new ZoneTemperature(0, 0)}, //ImOnABoat
            {288, new ZoneTemperature(0, 0)}, //ImOnABoat
            {292, new ZoneTemperature(100, 113)}, //[Trial]Bowl of Embers
            {331, new ZoneTemperature(0, 0)}, //Garuda_Entrance
            {332, new ZoneTemperature(0, 0)}, //[Trial]CapeWestwind
            {336, coastal}, //Wolves Den
            {337, coastal}, //Wolves Den
            {340, temperateForest}, //Lavender Beds
            {341, new ZoneTemperature(60, 91)}, //The Goblet
            {345, indoors}, //Private Cottage - The Goblet
            {349, new ZoneTemperature(0, 0)}, //[Dungeon]CopperbellHM
            {350, new ZoneTemperature(0, 0)}, //[Dungeon]HaukkeHM
            {351, indoors}, //Rising Stones
            {352, coastal}, //Wolves Den
            {360, new ZoneTemperature(0, 0)}, //[Dungeon]HalataliHM
            {361, new ZoneTemperature(0, 0)}, //[Dungeon]HullbreakerIsle
            {362, new ZoneTemperature(0, 0)}, //[Dungeon]BrayfloxHM
            {363, new ZoneTemperature(0, 0)}, //[Dungeon]LostCity
            {365, new ZoneTemperature(0, 0)}, //[Dungeon]StoneVigilHM
            {367, new ZoneTemperature(0, 0)}, //[Dungeon]SunkenTempleHM
            {371, new ZoneTemperature(0, 0)}, //[Dungeon]Snowcloak
            {373, new ZoneTemperature(0, 0)}, //[Dungeon]TamTaraHM
            {374, new ZoneTemperature(0, 0)}, //[Trial]Ramuh
            {376, new ZoneTemperature(0, 0)}, //Frontlines
            {377, new ZoneTemperature(0, 0)}, //[Trial]Shiva
            {387, new ZoneTemperature(0, 0)}, //[Dungeon]SastashaHM
            {388, indoors}, //Gold Saucer - Chocobo Square
            {395, new ZoneTemperature(0, 0)}, //Intercessory
            {397, tundra}, //Coerthas Western Highlands
            {398, new ZoneTemperature(0, 0)}, //The Dravanian Forelands
            {399, new ZoneTemperature(0, 0)}, //The Dravanian Hinterlands
            {400, new ZoneTemperature(0, 0)}, //The Churning Mists
            {401, new ZoneTemperature(0, 0)}, //Sea of Clouds
            {402, new ZoneTemperature(0, 0)}, //Azys Lla
            {418, tundra}, //Ishgard - Foundation
            {419, tundra}, //Ishgard - The Pillars
            {421, new ZoneTemperature(0, 0)}, //[Dungeon]Vault
            {422, new ZoneTemperature(0, 0)}, //Frontlines - Slaughter
            {426, new ZoneTemperature(0, 0)}, //[Trial]Chrysalis
            {427, indoors}, //Ishgard - Scholasticate
            {433, indoors}, //Ishgard - Fortempts Manor
            {434, new ZoneTemperature(0, 0)}, //[Dungeon]DuskVigil
            {435, new ZoneTemperature(0, 0)}, //[Dungeon]Aery
            {439, new ZoneTemperature(0, 0)}, //Ishgard - Chocobo Proving Grounds
            {441, new ZoneTemperature(0, 0)}, //[Dungeon]SohmAl
            {456, new ZoneTemperature(0, 0)}, //Ishgard - Ruling Chamber
            {463, new ZoneTemperature(0, 0)}, //Matoyas Cave
            {478, new ZoneTemperature(0, 0)}, //Idyllshire
            {554, new ZoneTemperature(0, 0)}, //[PVP] - Fields of Glory (Shatter)
            {612, new ZoneTemperature(0, 0)}, //The Fringes
            {613, new ZoneTemperature(0, 0)}, //The Ruby Sea
            {614, new ZoneTemperature(0, 0)}, //Yanxia
            {620, new ZoneTemperature(0, 0)}, //The Peaks
            {621, new ZoneTemperature(0, 0)}, //The Lochs
            {622, new ZoneTemperature(0, 0)}, //The Azim Steppe
            {626, coastal}, //[Dungeon]The Sirensong Sea
            {628, new ZoneTemperature(0, 0)}, //Kugane
            {635, new ZoneTemperature(0, 0)}, //Rhalgrs Reach
            {639, new ZoneTemperature(0, 0)}, //Ruby Bazaar Offices
            {680, new ZoneTemperature(0, 0)}, //ImOnABoatAgain
            {681, new ZoneTemperature(0, 0)}, //The House of the Fierce
            {683, new ZoneTemperature(0, 0)}, //First Alter of Djanan
            {813, temperateForest}, //Lakeland
            {815, desert}, //Amh Araeng
            {816, shrubland}, //Il Mheg
            {817, rainForest}, //Rak'tika Greatwood
            {818, new ZoneTemperature(30, 38)}, //The Tempest
            {819, temperateForest}, //The Crystarium
            {957, desert}, //Thavnair
            {963, desert}, //Radz-at-Han
            {956, indoors}, //Labyrinthos
            {958, tundra}, //Garlemald
            {959, new ZoneTemperature(63, 63)}, //Mare Lamentorum
            {1036, new ZoneTemperature(77, 77)}, //[Dungeon]Sastasha (New?)
            {1041, coastal}, //[Dungeon]Brayflox (New?)
            {1063, new ZoneTemperature(30, 50)}, //[Dungeon]KeepersOfTheLake (New?)
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
            {3, -2}, //Clouds
            {4, -8}, //Fog
            {5, -2}, //Wind
            {6, -3}, //Gales
            {7, -6}, //Rain
            {8, -8}, //Shower
            {9, -2}, //Thunder
            {10, -10}, //Thunderstorms
            {11, 1}, //Dust Storms
            {12, 2}, //Sandstorms
            {13, 6}, //Hot Spells
            {14, 10}, //Heat Waves
            {15, -6}, //Snows
            {16, -10}, //Blizzards
            {17, -3}, //Gloom
        };

        public static int GetTemperature(ushort zoneId, float time)
        {
            float perc;
            if (time < coldestHour)
            {
                perc = 1 - ((time + 24 - hottestHour) / (coldestHour + 24 - hottestHour));
            }
            else if (time > hottestHour)
            {
                perc = 1 - ((time - hottestHour) / (24 + coldestHour - hottestHour));
            }
            else
            {
                perc = (time - coldestHour) / (hottestHour - coldestHour);
            }
            //int temp = (int)Math.Round(perc * (temperatures[zoneId].high - temperatures[zoneId].low) + temperatures[zoneId].low);
            //PluginLog.Information($"{perc}: {SinDistribution(perc, 0.5, 2.0, temperatures[zoneId].low, temperatures[zoneId].high)}");
            int temp = (int)Math.Round(SinDistribution(perc, 0.5, 2.0, zoneTemperatures[zoneId].low, zoneTemperatures[zoneId].high));
            //PluginLog.Information($"{perc + ((perc - 0.5) * 0.5)}");

            return temp;
        }

        private static double SinDistribution(double value, double lowToHighMeanPoint, double length, double low, double high)
        {
            var amplitude = (high - low) / 2;
            var mean = low + amplitude;
            //PluginLog.Information($"({value} - {lowToHighMeanPoint}) % {length}) = {((value - lowToHighMeanPoint) % length).ToString()}");
            return mean + (amplitude * Math.Sin(
                (((value - lowToHighMeanPoint) % length) / length) * 2 * Math.PI));
        }
    }
}
