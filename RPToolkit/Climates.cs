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

        #region biomeTemps
        private static int indoorHighTemp = 76;
        private static int indoorLowTemp = 68;

        private static int desertHighTemp = 100;
        private static int desertLowTemp = 25;

        private static int coastalHighTemp = 86;
        private static int coastalLowTemp = 68;

        private static int shrublandHighTemp = 79;
        private static int shrublandLowTemp = 55;

        private static int tundraHighTemp = 27;
        private static int tundraLowTemp = 2;
        #endregion

        public struct ZoneTemperature
        {
            public int low;
            public int high;

            public ZoneTemperature(int high, int low)
            {
                this.high = high;
                this.low = low;
            }
        }

        public static Dictionary<ushort, ZoneTemperature> temperatures { get; private set; } = new Dictionary<ushort, ZoneTemperature>
        {
            // Zone ID, Avg High Temp, Avg Low Temp (Yes, in the end I've decided to use Fahrenheit. I don't feel like using floats.)
            {128, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Limsa (Upper)
            {129, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Limsa (Lower)
            {130, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Ul dah - Steps of Nald
            {131, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Ul dah - Steps of Thal
            {132, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //New Gridania
            {133, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //Old Gridania
            {134, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Middle La Noscea
            {135, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Lower La Noscea
            {137, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Eastern La Noscea
            {138, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Western La Noscea
            {139, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Upper La Noscea
            {140, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Western Thanalan
            {141, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Central Thanalan
            {144, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Gold Saucer
            {145, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Eastern Thanalan
            {146, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Southern Thanalan
            {147, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Northern Thanalan
            {148, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //Central Shroud
            {150, new ZoneTemperature(30, 50)}, //[Dungeon]KeepersOfTheLake
            {152, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //East Shroud
            {153, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //South Shroud
            {154, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //North Shroud
            {155, new ZoneTemperature(tundraHighTemp, tundraLowTemp)}, //Coerthas
            {156, new ZoneTemperature(30, 50)}, //Mor Dhona
            /*{157, new ZoneTemperature(0, 0)}, //[Dungeon]Sastasha
            {158, new ZoneTemperature(0, 0)}, //[Dungeon]Brayflox
            {159, new ZoneTemperature(0, 0)}, //[Dungeon]WanderersPalace
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
            {172, new ZoneTemperature(0, 0)}, //[Dungeon]AurumVale*/
            {175, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Wolves Den
            {177, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Limsa Lominsa - Inn
            {178, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Ul dah - Inn
            {179, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Gridania - Inn
            {180, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Outer La Noscea
            {181, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Limsa (Upper)
            {182, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Ul dah - Steps of Nald
            {183, new ZoneTemperature(shrublandHighTemp, shrublandLowTemp)}, //New Gridania
            {186, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Wolves Den
            {198, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Limsa Lominsa - Command
            //{202, new ZoneTemperature(0, 0)}, //[Trial]Ifrit
            //{204, new ZoneTemperature(0, 0)}, //Gridania - First Bow
            //{205, new ZoneTemperature(0, 0)}, //Lotus Stand
            //{206, new ZoneTemperature(0, 0)}, //[Trial]Titan
            //{207, new ZoneTemperature(0, 0)}, //[Trial]MoogleMog
            //{208, new ZoneTemperature(0, 0)}, //[Trial]Garuda
            {210, new ZoneTemperature(desertHighTemp, desertLowTemp)}, //Ul dah - Heart of the Sworn
            {212, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Waking Sands
            {250, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Wolves Den Pier
            //{281, new ZoneTemperature(0, 0)}, //[Trial]Leviathan
            //{286, new ZoneTemperature(0, 0)}, //ImOnABoat
            //{288, new ZoneTemperature(0, 0)}, //ImOnABoat
            //{331, new ZoneTemperature(0, 0)}, //Garuda_Entrance
            //{332, new ZoneTemperature(0, 0)}, //[Trial]CapeWestwind
            {336, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Wolves Den
            {337, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Wolves Den
            {341, new ZoneTemperature(91, 60)}, //The Goblet
            {345, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Private Cottage - The Goblet
            //{349, new ZoneTemperature(0, 0)}, //[Dungeon]CopperbellHM
            //{350, new ZoneTemperature(0, 0)}, //[Dungeon]HaukkeHM
            {351, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Rising Stones
            {352, new ZoneTemperature(coastalHighTemp, coastalLowTemp)}, //Wolves Den
            /*{360, new ZoneTemperature(0, 0)}, //[Dungeon]HalataliHM
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
            {387, new ZoneTemperature(0, 0)}, //[Dungeon]SastashaHM*/
            {388, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Gold Saucer - Chocobo Square
            //{395, new ZoneTemperature(0, 0)}, //Intercessory
            {397, new ZoneTemperature(tundraHighTemp, tundraLowTemp)}, //Coerthas Western Highlands
            {398, new ZoneTemperature(0, 0)}, //The Dravanian Forelands
            {399, new ZoneTemperature(0, 0)}, //The Dravanian Hinterlands
            {400, new ZoneTemperature(0, 0)}, //The Churning Mists
            {401, new ZoneTemperature(0, 0)}, //Sea of Clouds
            {402, new ZoneTemperature(0, 0)}, //Azys Lla
            {418, new ZoneTemperature(tundraHighTemp, tundraLowTemp)}, //Ishgard - Foundation
            {419, new ZoneTemperature(tundraHighTemp, tundraLowTemp)}, //Ishgard - The Pillars
            /*{421, new ZoneTemperature(0, 0)}, //[Dungeon]Vault
            {422, new ZoneTemperature(0, 0)}, //Frontlines - Slaughter
            {426, new ZoneTemperature(0, 0)}, //[Trial]Chrysalis*/
            {427, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Ishgard - Scholasticate
            {433, new ZoneTemperature(indoorHighTemp, indoorLowTemp)}, //Ishgard - Fortempts Manor
            /*{434, new ZoneTemperature(0, 0)}, //[Dungeon]DuskVigil
            {435, new ZoneTemperature(0, 0)}, //[Dungeon]Aery
            {439, new ZoneTemperature(0, 0)}, //Ishgard - Chocobo Proving Grounds
            {441, new ZoneTemperature(0, 0)}, //[Dungeon]SohmAl
            {456, new ZoneTemperature(0, 0)}, //Ishgard - Ruling Chamber*/
            {463, new ZoneTemperature(0, 0)}, //Matoyas Cave
            {478, new ZoneTemperature(0, 0)}, //Idyllshire
            //{554, new ZoneTemperature(0, 0)}, //[PVP] - Fields of Glory (Shatter)
            {612, new ZoneTemperature(0, 0)}, //The Fringes
            {613, new ZoneTemperature(0, 0)}, //The Ruby Sea
            {614, new ZoneTemperature(0, 0)}, //Yanxia
            {620, new ZoneTemperature(0, 0)}, //The Peaks
            {621, new ZoneTemperature(0, 0)}, //The Lochs
            {622, new ZoneTemperature(0, 0)}, //The Azim Steppe
            {628, new ZoneTemperature(0, 0)}, //Kugane
            {635, new ZoneTemperature(0, 0)}, //Rhalgrs Reach
            {639, new ZoneTemperature(0, 0)}, //Ruby Bazaar Offices
            //{680, new ZoneTemperature(0, 0)}, //ImOnABoatAgain
            {681, new ZoneTemperature(0, 0)}, //The House of the Fierce
            {683, new ZoneTemperature(0, 0)} //First Alter of Djanan
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
            int temp = (int)Math.Round(SinDistribution(perc, 0.5, 2.0, temperatures[zoneId].low, temperatures[zoneId].high));
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
