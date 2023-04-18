using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Data
{
    internal class Consumables
    {
        //Replace <food> string with name of food, <temp> with temperature of food, <type> with type of food.
        #pragma warning disable IDE1006 // Naming Styles
        private static string heartyMeal = "As you eat the <temp> and filling <food>, you are hit with the savory flavors, creating a delicious and comforting meal that satisfies your hunger and warms your soul.";
        private static string lightMeal = "As you eat the <temp> and flavorful <food>, you can taste the light flavors of the dish, creating a light and healthy meal that leaves you feeling revitalized.";
        private static string sweetTreat = "As you taste the <temp> <food>, you are hit with a mouth full of sweet flavors, creating a delectable and comforting treat that is perfect for indulging your sweet tooth.";
        private static string snack = "You munch on the <food>, feeling satisfied for the moment before you find yourself reaching for more.";
        private static string sweetDrink = "You take a sip from the <temp> and sweet <food>, feeling refreshed and satisfying any sweet cravings you may have had.";
        private static string savoryDrink = "As you take a sip of the <temp> <food>, you are hit with a taste of slight sweetness yet subtle hint of salt, creating a refreshing yet savory drink.";
        private static string bitterDrink = "As you take a sip of the <temp> and bitter <food>, you are hit with a strong and bold flavor, perking you up.";
        private static string disgusting = "You slowly take a bite of the <food>, you are immediately hit with an overpowering and unpleasant taste, accompanied by a strong and lingering aftertaste. At least it's 'healthy', right?"; //Archon Loaf...
        private static string heartySoup = "You begin to eat the <temp> and savory bowl of <food>, you taste of rich and comforting flavors, along with a <temp> and satisfying texture of the broth.";
        private static string salad = "As you eat the refreshing and <temp> <food>, you can taste the crispness of the lettuce, the rich flavors of the toppings, and the tanginess of the dressing, creating a light and flavorful meal that refreshes your palate and leaves you feeling satisfied.";
        private static string genericSpicy = "You eat the spicy <food>, feeling the fiery heat spread through your mouth and down your throat, making your eyes water and your tongue tingle with intense flavor.";

        private static Consumable steak = new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal);
        private static Consumable warmHeartySoup = new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartySoup);
        #pragma warning restore IDE1006 // Naming Styles

        public static Dictionary<uint, Consumable> consumables = new Dictionary<uint, Consumable>
        {
            // ID, (Fulfillment, Temperature, Description)
            {4644, steak}, //Aldgoat Steak
            {4701, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat)}, //Acorn Cookie
            {4685, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, salad)}, //Alligator Salad
            {12852, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack)}, //Almond Cream Croissant
            {36039, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, sweetDrink)}, //Amra Lassi
            {36038, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, salad)}, //Amra Salad
            {14140, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal)}, //Angler Stew
            {4643, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal)}, //Antelope Steak
            {4675, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal)}, //Antelope Stew
            {4656, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Apkallu Omelette
            {4747, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, sweetDrink) }, //Apple Juice
            {14135, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Apple Strudel
            {4709, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Apple Tart
            {36067, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, //Archon Burger
                "As you take a bite of the Archon Burger, you can feel the tender juiciness of the meat, the softness of the bun, the melting creaminess of the cheese, and the burst of flavors from the toppings and sauce, all combining to create a satisfying and mouth-watering taste.") },
            {36036, new Consumable(FoodType.HeartyMeal, FoodTemp.Lukewarm, disgusting) }, //Archon Loaf
            {28721, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Arros Negre
            {9331, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack) }, //Bacon Bread
            {9335, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal)}, //Bacon Broth
            {27860, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal)}, //Baguette
            {36063, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Baked Alien Soup
            {27869, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Baked Megapiranha
            {12861, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Baked Onion Soup
            {12856, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Baked Pipira Pira
            {4669, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Baked Sole
            {19811, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Baklava
            {23186, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Banh Xeo
            {10332, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Battered Fish (Fish & Chips)
            {4678, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Beef Stew
            {36068, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Beef Stroganoff
            {12862, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Beet Soup
            {10146, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Better Crowned Pie
            {6961, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Black Drop (Candy)
            {4695, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Black Truffle Risotto
            {27876, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Blood Bouillabaisse
            {4712, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Blood Currant Tart
            {27861, new Consumable(FoodType.LightDrink, FoodTemp.Cold, savoryDrink) }, //Blood Tomato Juice
            {27864, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Blood Tomato Salad
            {6957, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Blue Drop (Candy)
            {19823, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Boiled Amberjack Head
            {4668, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Boiled Bream
            {4659, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Boiled Crayfish
            {4650, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack) }, //Boiled Egg
            {36043, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Borscht
            {14137, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Boscaiola
            {4721, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Bouillabaisse
            {4657, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Braised Pipira
            {31906, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Broad Bean Curry
            {27858, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Broad Bean Salad
            {27856, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Broad Bean Soup
            {4735, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Bubble Chocolate
            {19809, new Consumable(FoodType.LightDrink, FoodTemp.Warm, bitterDrink) }, //Buckwheat Tea
            {20932, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Buttery Mogbiscuit
            {4693, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Button Mushroom Saute
            {4694, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Buttons in a Blanket
            {37282, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Calamari Ripieni
            {27855, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Caramels
            {36047, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Carrot Nibbles
            {38264, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Carrot Pudding (What would carrot pudding even taste like???)
            {4719, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Cawl Cennin
            {4749, new Consumable(FoodType.LightDrink, FoodTemp.Warm, sweetDrink) }, //Chamomile Tea
            {4689, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Chanterelle Saute
            {19828, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Charred Charr
            {21088, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Chawan-mushi
            {4723, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Cheese Risotto
            {4724, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Cheese Souffle
            {4690, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Chicken and Mushrooms
            {31900, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Chicken Fettuccine
            {30482, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Chili Crab
            {7574, new Consumable(FoodType.LightMeal, FoodTemp.Cold, lightMeal) }, //Chilled Popoto Soup
            {19814, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Chirashi-zushi
            {12864, new Consumable(FoodType.HeartyMeal, FoodTemp.Lukewarm, heartySoup) }, //Clam Chowder
            {6958, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Clear Drop (Candy)
            {12858, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Cockatrice Meatballs
            {36057, warmHeartySoup }, //Coconut Cod Chowder
            {27878, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack) }, //Coffee Biscuit
            {4739, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Consecrated Chocolate
            {4700, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack) }, //Cornbread
            {30481, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Crab Cakes
            {19833, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Crab Croquette
            {27883, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Creamy Salmon Pasta
            {12848, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Creme Brulee
            {22436, new Consumable(FoodType.LightDrink, FoodTemp.Cold, sweetDrink) }, //Crimson Cider
            {4713, new Consumable(FoodType.Snack, FoodTemp.Warm, sweetTreat) }, //Crowned Pie
            {4732, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, sweetTreat) }, //Crumpet (Pancakes)
            {6144, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, //Dagger Soup
                "You taste the <temp> soup, you can taste the delicate fish flavor of the broth, the heartiness of the meatballs, and the richness of the overall flavor, making for a delicious and satisfying culinary experience.") },
            {4699, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack) }, //Dark Pretzel
            {12860, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Deep-fried Okeanis
            {4655, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, snack) }, //Deviled Eggs
            {12869, warmHeartySoup }, //Dhalmel Fricassee
            {12867, warmHeartySoup }, //Dhalmel Gratin
            {4651, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, lightMeal) }, //Dodo Omelette
            {19807, new Consumable(FoodType.LightDrink, FoodTemp.Cold, sweetDrink) }, //Doman Tea
            {4730, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, sweetTreat) }, //Dried Plums
            {4679, warmHeartySoup }, //Dzemael Gratin
            {22435, steak }, //Dzo Steak
            {4711, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, //Eel Pie
                "What may seem like an odd pie at first, as you take a bite of the eel pie, you taste the savory and slightly salty flavor of the plump, protein-rich eel filling. The flaky crust complements the meaty texture of the eel, creating a perfect balance of flavors and textures.") },
            {4647, steak }, //Eft Steak
            {19820, new Consumable(FoodType.LightMeal, FoodTemp.Warm, lightMeal) }, //Egg Foo Young
            {36062, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartyMeal) }, //Elpis Deipnon
            {23188, new Consumable(FoodType.HeartyMeal, FoodTemp.Hot, genericSpicy) }, //Ema Datshi
        };

        public struct Consumable
        {
            public FoodType type;
            public FoodTemp temp;
            public string flavorText;

            public Consumable(FoodType type, FoodTemp temp, string flavorText)
            {
                this.type = type;
                this.temp = temp;
                this.flavorText = flavorText;
            }
        }

        public enum FoodType
        {
            HeartyMeal,
            LightMeal,
            Snack,
            LightDrink,
            RefreshingDrink,
        }

        public enum FoodTemp
        {
            Hot,
            Warm,
            Lukewarm,
            Cold
        }
    }
}
