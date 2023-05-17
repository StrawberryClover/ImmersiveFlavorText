using Dalamud;
using RPToolkit.Localizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Localization = RPToolkit.Localizations.Localization;

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
        private static string heartySoup = "You take a taste of the <temp> and savory bowl of <food>, the flavors are rich and comforting, and you are met with a <temp> and satisfying texture of the broth.";
        private static string salad = "As you eat the refreshing and <temp> <food>, you can taste the crispness of the lettuce, the rich flavors of the toppings, and the tanginess of the dressing, creating a light and flavorful meal that refreshes your palate and leaves you feeling satisfied.";
        private static string genericSpicy = "You eat the spicy <food>, feeling the fiery heat spread through your mouth and down your throat, making your eyes water and your tongue tingle with intense flavor.";
        private static string altSpicy = "As you take a bite of the spicy <food>, a tingling sensation spreads across your tongue, followed by a burst of heat that intensifies with each passing second. Your mouth feels alive with a fiery flavor, leaving you breathless yet craving for more.";
        private static string breakfast = "As you take a bite of your <food>, you taste a savory and slightly salty flavor with a hint of sweetness. The texture is soft and fluffy with a slight crunch on the outside. It feels like it would be the perfect start to a day.";

        private static Consumable steak = new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.steak);
        private static Consumable grilledMeat = new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.grilledMeat); //Basically just steak lmao
        private static Consumable grilledFish = new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.grilledFish);
        private static Consumable warmHeartySoup = new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, heartySoup);
#pragma warning restore IDE1006 // Naming Styles

        public static Dictionary<uint, Consumable> consumables = new Dictionary<uint, Consumable>
        {
            // ID, (Fulfillment, Temperature, Description)
            {4644, steak}, //Aldgoat Steak
            {4701, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.acornCookie)}, //Acorn Cookie
            {4685, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.alligatorSalad)}, //Alligator Salad
            {12852, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.almondCreamCroissant)}, //Almond Cream Croissant
            {36039, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.amraLassi)}, //Amra Lassi
            {36038, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.amraSalad)}, //Amra Salad
            {14140, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.anglerStew)}, //Angler Stew
            {4643, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.steak)}, //Antelope Steak
            {4675, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.antelopeStew)}, //Antelope Stew
            {4656, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.omelette) }, //Apkallu Omelette
            {4747, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.appleJuice) }, //Apple Juice
            {14135, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.appleStrudel) }, //Apple Strudel
            {4709, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.appleTart) }, //Apple Tart
            {36067, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.archonBurger) }, //Archon Burger
            {36036, new Consumable(FoodType.HeartyMeal, FoodTemp.Lukewarm, Localization.lang.consumables.archonLoaf) }, //Archon Loaf
            {28721, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.arrosNegre) }, //Arros Negre
            {9331, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.baconBread) }, //Bacon Bread
            {9335, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.baconBroth)}, //Bacon Broth
            {27860, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.baguette)}, //Baguette
            {36063, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.bakedOnionSoup) }, //Baked Alien Soup
            {27869, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.bakedMegapiranha) }, //Baked Megapiranha
            {12861, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.bakedOnionSoup) }, //Baked Onion Soup
            {12856, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.bakedPipiraPira) }, //Baked Pipira Pira
            {4669, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.bakedSole) }, //Baked Sole
            {19811, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.baklava) }, //Baklava
            {23186, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.banhXeo) }, //Banh Xeo
            {10332, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.batteredFish) }, //Battered Fish (Fish & Chips)
            {4678, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.beefStew) }, //Beef Stew
            {36068, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.beefStroganoff) }, //Beef Stroganoff
            {12862, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.beetSoup) }, //Beet Soup
            {10146, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.crownedPie) }, //Better Crowned Pie
            {6961, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.candyDrop) }, //Black Drop (Candy)
            {4695, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.blackTruffleRisotto) }, //Black Truffle Risotto
            {27876, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.bouillabaisse) }, //Blood Bouillabaisse
            {4712, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.bloodCurrantTart) }, //Blood Currant Tart
            {27861, new Consumable(FoodType.LightDrink, FoodTemp.Cold, Localization.lang.consumables.tomatoJuice) }, //Blood Tomato Juice
            {27864, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.tomatoSalad) }, //Blood Tomato Salad
            {6957, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.candyDrop) }, //Blue Drop (Candy)
            {19823, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.boiledAmberjackHead) }, //Boiled Amberjack Head
            {4668, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.boiledBream) }, //Boiled Bream
            {4659, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.boiledCrayfish) }, //Boiled Crayfish
            {4650, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.boiledEgg) }, //Boiled Egg
            {36043, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.borscht) }, //Borscht
            {14137, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.boscaiola) }, //Boscaiola
            {4721, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.bouillabaisse) }, //Bouillabaisse
            {4657, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.braisedPipira) }, //Braised Pipira
            {31906, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.broadBeanCurry) }, //Broad Bean Curry
            {27858, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.broadBeanSalad) }, //Broad Bean Salad
            {27856, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.broadBeanSoup) }, //Broad Bean Soup
            {4735, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.chocolate) }, //Bubble Chocolate
            {19809, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.buckwheatTea) }, //Buckwheat Tea
            {20932, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.butteryMogbiscuit) }, //Buttery Mogbiscuit
            {4693, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.buttonMushroomSaute) }, //Button Mushroom Saute
            {4694, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.buttonsInABlanket) }, //Buttons in a Blanket
            {37282, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.calamariRipieni) }, //Calamari Ripieni
            {27855, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.caramels) }, //Caramels
            {36047, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.carrotNibbles) }, //Carrot Nibbles
            {38264, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.carrotPudding) }, //Carrot Pudding (What would carrot pudding even taste like???)
            {4719, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.cawlCennin) }, //Cawl Cennin
            {4749, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.chamomileTea) }, //Chamomile Tea
            {4689, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.chanterelleSaute) }, //Chanterelle Saute
            {19828, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.charredCharr) }, //Charred Charr
            {21088, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.chawanmushi) }, //Chawan-mushi
            {4723, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.cheeseRisotto) }, //Cheese Risotto
            {4724, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.cheeseSouffle) }, //Cheese Souffle
            {4690, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.chickenAndMushrooms) }, //Chicken and Mushrooms
            {31900, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.chickenFettuccine) }, //Chicken Fettuccine
            {30482, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.chiliCrab) }, //Chili Crab
            {7574, new Consumable(FoodType.LightMeal, FoodTemp.Cold, Localization.lang.consumables.chilledPopotoSoup) }, //Chilled Popoto Soup
            {19814, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.chirashizushi) }, //Chirashi-zushi
            {12864, new Consumable(FoodType.HeartyMeal, FoodTemp.Lukewarm, Localization.lang.consumables.clamChowder) }, //Clam Chowder
            {6958, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.candyDrop) }, //Clear Drop (Candy)
            {12858, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.cockatriceMeatballs) }, //Cockatrice Meatballs
            {36057, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.coconutCodChowder) }, //Coconut Cod Chowder
            {27878, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.coffeeBiscuit) }, //Coffee Biscuit
            {4739, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.chocolate) }, //Consecrated Chocolate
            {4700, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.cornbread) }, //Cornbread
            {30481, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.crabCakes) }, //Crab Cakes
            {19833, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.crabCroquette) }, //Crab Croquette
            {27883, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.creamySalmonPasta) }, //Creamy Salmon Pasta
            {12848, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.cremeBrulee) }, //Creme Brulee
            {22436, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.crimsonCider) }, //Crimson Cider
            {4713, new Consumable(FoodType.Snack, FoodTemp.Warm, Localization.lang.consumables.crownedPie) }, //Crowned Pie
            {4732, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.crumpet) }, //Crumpet (Pancakes)
            {6144, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.daggerSoup) }, //Dagger Soup
            {4699, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.darkPretzel) }, //Dark Pretzel
            {12860, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.deepfriedOkeanis) }, //Deep-fried Okeanis
            {4655, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.deviledEggs) }, //Deviled Eggs
            {12869, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.dhalmelFricassee) }, //Dhalmel Fricassee
            {12867, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.dhalmelGratin) }, //Dhalmel Gratin
            {4651, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.omelette) }, //Dodo Omelette
            {19807, new Consumable(FoodType.LightDrink, FoodTemp.Cold, Localization.lang.consumables.domanTea) }, //Doman Tea
            {4730, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.driedPlums) }, //Dried Plums
            {4679, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.dzemaelGratin) }, //Dzemael Gratin
            {22435, steak }, //Dzo Steak
            {4711, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.eelPie) }, //Eel Pie
            {4647, steak }, //Eft Steak
            {19820, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.eggFooYoung) }, //Egg Foo Young
            {36062, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.elpisDeipnon) }, //Elpis Deipnon
            {23188, new Consumable(FoodType.HeartyMeal, FoodTemp.Hot, Localization.lang.consumables.emaDatshi) }, //Ema Datshi
            {12863, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.emeraldSoup) }, //Emerald Soup
            {27874, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.espressoConPanna) }, //Espresso con Panna
            {27892, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.exquisiteBeefStew) }, //Exquisite Beef Stew
            {29505, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.farmersBreakfast) }, //Farmer's Breakfast
            {20931, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.chocolate) }, //Fat Choco Choco
            {12845, new Consumable(FoodType.LightMeal, FoodTemp.Cold, Localization.lang.consumables.figBavarois)}, //Fig Bavarois
            {4704, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.fingerSandwich)}, //Finger Sandwich
            {4720, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.fishSoup)}, //Fish Soup
            {19835, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.fishStew)}, //Fish Stew
            {4696, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.flatbread)}, //Flatbread
            {16710, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.flaugnarde)}, //Flaugnarde
            {9332, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.flintCaviar)}, //Flint Caviar
            {4691, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.forestMiqabob)}, //Forest Miqabob
            {4653, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.friedEgg)}, //Fried Egg
            {12843, new Consumable(FoodType.LightDrink, FoodTemp.Cold, Localization.lang.consumables.frozenSpirits)}, //Frozen Spirits
            {4716, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.frumenty)}, //Frumenty
            {22433, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.futomakiRoll)}, //Futomaki Roll
            {19831, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.gameni)}, //Gameni
            {38268, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.garleanPizza)}, //Garlean Pizza
            {36053, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.giantHaddockDip)}, //Giant Haddock Dip
            {36059, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.giantPopotoPancakes)}, //Giant Popoto Pancakes
            {4731, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.gingerCookie)}, //Ginger Cookie
            {24275, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.gingerSalad)}, //Ginger Salad
            {36045, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.gloryBeSoup)}, //Glory Be Soup
            {31902, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.goldenPineappleJuice)}, //Golden Pineapple Juice
            {4746, new Consumable(FoodType.LightDrink, FoodTemp.Cold, Localization.lang.consumables.grapeJuice)}, //Grape Juice
            {6963, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.candyDrop)}, //Green Drop
            {4661, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.grilledCarp)}, //Grilled Carp
            {4640, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.grilledDodo)}, //Grilled Dodo
            {27859, grilledFish}, //Grilled Platinum Bream
            {27853, grilledMeat}, //Grilled Rail
            {6143, grilledFish}, //Grilled Raincaller
            {12855, grilledFish}, //Grilled Sweetfish
            {4660, grilledFish}, //Grilled Trout
            {19822, new Consumable(FoodType.Snack, FoodTemp.Warm, Localization.lang.consumables.grilledTurban)}, //Grilled Turban
            {6142, grilledFish}, //Grilled Warmwater Trout
            {38265, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.gyros)}, //Gyros
            {9334, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.haddockDip)}, //Haddock Dip
            {36048, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.hamsaCurry)}, //Hamsa Curry
            {36046, new Consumable(FoodType.LightDrink, FoodTemp.Cold, Localization.lang.consumables.happinessJuice)}, //Happiness Juice
            {4738, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.chocolate)}, //Heart Chocolate
            {16709, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.heavenlyEggnog)}, //Heavenly Eggnog
            {15650, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.heavenseggSoup)}, //Heavensegg Soup
            {29504, new Consumable(FoodType.LightDrink, FoodTemp.Lukewarm, Localization.lang.consumables.herringPie)}, //Herring Pie
            {17576, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.honeyBun)}, //Honey Bun
            {29500, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.honeyCroissant)}, //Honey Croissant
            {4698, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.honeyMuffin)}, //Honey Muffin
            {12844, new Consumable(FoodType.RefreshingDrink, FoodTemp.Hot, Localization.lang.consumables.hotChocolate)}, //Hot Chocolate
            {4707, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.hourglassBiscuit)}, //Hourglass Biscuit
            {24277, new Consumable(FoodType.LightMeal, FoodTemp.Hot, Localization.lang.consumables.imamBayildi)}, //Imam Bayildi
            {12850, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.ishgardianMuffin)}, //Ishgardian Muffin
            {12842, new Consumable(FoodType.LightDrink, FoodTemp.Hot, Localization.lang.consumables.ishgardianTea)}, //Ishgardian Tea
            {36040, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.islandMiqabob)}, //Island Miqabob
            {4681, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.jackolantern)}, //Jackolantern
            {19812, new Consumable(FoodType.Snack, FoodTemp.Cold, Localization.lang.consumables.jelliedCompote)}, //Jellied Compote
            {27881, new Consumable(FoodType.Snack, FoodTemp.Cold, Localization.lang.consumables.jelliedHarcot)}, //Jellied Harcot
            {4646, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.jerkedBeef)}, //Jerked Beef
            {19819, new Consumable(FoodType.Snack, FoodTemp.Hot, Localization.lang.consumables.jerkedJhammel)}, //Jerked Jhammel
            {19830, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.jhammelMoussaka)}, //Jhammel Moussaka
            {38929, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.jhingaBiryani)}, //Jhinga Biryani
            {38930, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.jhingaCurry)}, //Jhinga Curry
            {12849, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.kaiserRoll)}, //Kaiser Roll
            {37283, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.kalamarakiaTiganita)}, //Kalamarakia Tiganita
            {36049, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.karniyarik)}, //Karniyarik
            {19838, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.kasha)}, //Kasha
            {36041, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.kingCrabCake)}, //King Crab Cake
            {27884, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.kingSalmonMeuniere)}, //King Salmon Meuniere
            {38270, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.kingUrchinLoaf)}, //King Urchin Loaf
            {13595, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.kingcake)}, //Kingcake
            {4702, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.knightsBread)}, //Knights Bread
            {23185, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.konpeito)}, //Konpeito
            {27889, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.kukuruRusk)}, //Kukuru Rusk
            {4703, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.laNosceanToast)}, //La Noscean Toast
            {24276, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.laghman)}, //Laghman
            {4688, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.landtrapSalad)}, //Landtrap Salad
            {4665, new Consumable(FoodType.HeartyMeal, FoodTemp.Hot, Localization.lang.consumables.lavaToadLegs)}, //Lava Toad Legs
            {27890, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.lemonCurdSachertorte)}, //Lemon Curd Sachertorte
            {31907, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.lemonMuffin)}, //Lemon Muffin
            {29499, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.lemonWaffle)}, //Lemon Waffle
            {27880, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.lemonade)}, //Lemonade
            {36051, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.lentilCurry)}, //Lentil Curry
            {4674, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.lentilsAndChestnuts)}, //Lentilsand Chestnuts
            {12851, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.livercheeseSandwich)}, //Livercheese Sandwich
            {16714, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.loaghtanCordonBleu)}, //Loaghtan Cordon Bleu
            {38266, steak}, //Loaghtan Rump Steak
            {14138, steak}, //Loaghtan Steak
            {19808, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.loquatJuice)}, //Loquat Juice
            {4639, steak}, //Marmot Steak
            {12847, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.marronGlace)}, //Marron Glace
            {27872, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.masalaChai)}, //Masala Chai
            {4722, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.mashedPopotoes)}, //Mashed Popotoes
            {23187, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.matcha)}, //Matcha
            {4642, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.meatMiqabob)}, //Meat Miqabob
            {28720, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.mejillonesalAjillo)}, //Mejillonesal Ajillo
            {38262, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.melonJuice)}, //Melon Juice
            {38261, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.melonPie)}, //Melon Pie
            {4726, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.mintLassi)}, //Mint Lassi
            {19829, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.misoDengaku)}, //Miso Dengaku
            {19834, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.misoSoupWithTofu)}, //MisoSoup With Tofu
            {31899, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.mistSpinachQuiche)}, //Mist Spinach Quiche
            {27875, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.mistSpinachSaute)}, //Mist Spinach Saute
            {4705, new Consumable(FoodType.Snack, FoodTemp.Lukewarm, Localization.lang.consumables.mizzenmastBiscuit)}, //Mizzenmast Biscuit
            {4641, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.moleLoaf)}, //Mole Loaf
            {12854, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.morelSalad)}, //Morel Salad
            {29498, new Consumable(FoodType.RefreshingDrink, FoodTemp.Cold, Localization.lang.consumables.mors)}, //Mors
            {4663, grilledFish}, //Mugwort Carp
            {4750, new Consumable(FoodType.LightDrink, FoodTemp.Warm, Localization.lang.consumables.mulledTea)}, //Mulled Tea
            {27865, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.mushroomSaute)}, //Mushroom Saute
            {27868, new Consumable(FoodType.LightMeal, FoodTemp.Warm, Localization.lang.consumables.mushroomSkewer)}, //Mushroom Skewer
            {4652, new Consumable(FoodType.LightMeal, FoodTemp.Lukewarm, Localization.lang.consumables.mustardEggs)}, //Mustard Eggs
            {4673, new Consumable(FoodType.HeartyMeal, FoodTemp.Warm, Localization.lang.consumables.muttonStew)}, //Mutton Stew
            //{19839, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.nomadMeatPie)}, //Nomad Meat Pie
            //{27863, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.nutBake)}, //Nut Bake
            //{22434, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.oden)}, //Oden
            //{19821, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.onigarayaki)}, //Onigarayaki
            //{4745, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.orangeJuice)}, //Orange Juice
            //{4741, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.oreFruitcake)}, //Ore Fruitcake
            //{4676, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.orobonStew)}, //Orobon Stew
            //{27879, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.ovimCordonBleu)}, //Ovim Cordon Bleu
            //{29497, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.ovimMeatballs)}, //Ovim Meatballs
            //{14139, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.oysterConfit)}, //Oyster Confit
            //{27882, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.oystersontheHalfShell)}, //Oystersonthe Half Shell
            //{27877, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.paella)}, //Paella
            //{4672, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.panfriedMahimahi)}, //Panfried Mahimahi
            //{24278, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.papanasi)}, //Papanasi
            //{4682, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.parsnipSalad)}, //Parsnip Salad
            //{15651, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pastaOrtolano)}, //Pasta Ortolano
            //{4733, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pastryFish)}, //Pastry Fish
            //{4718, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.peaSoup)}, //Pea Soup
            //{36071, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.peachJuice)}, //Peach Juice
            //{36072, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.peachTart)}, //Peach Tart
            //{4736, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pearlChocolate)}, //Pearl Chocolate
            //{14136, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.peperoncino)}, //Peperoncino
            //{27870, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pepperedPopotoes)}, //Peppered Popotoes
            //{33885, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pepperoniPizza)}, //Pepperoni Pizza
            //{19815, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.persimmonLeafSushi)}, //Persimmon Leaf Sushi
            //{19813, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.persimmonPudding)}, //Persimmon Pudding
            //{36055, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.philosophersSandwich)}, //Philosophers Sandwich
            //{29503, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pickledHerring)}, //Pickled Herring
            //{38267, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.piennoloTomatoSalad)}, //Piennolo Tomato Salad
            //{4748, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pineappleJuice)}, //Pineapple Juice
            //{4734, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pineapplePonzecake)}, //Pineapple Ponzecake
            //{31903, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pineappleSalad)}, //Pineapple Salad
            //{27862, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pixieApplePie)}, //Pixie Apple Pie
            //{27888, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pixieberryCheesecake)}, //Pixieberry Cheesecake
            //{27887, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pixieberryTea)}, //Pixieberry Tea
            //{31898, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pizza)}, //Pizza
            //{7571, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.popotoPancakes)}, //Popoto Pancakes
            //{27885, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.popotoSalad)}, //Popoto Salad
            //{19816, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.popotoSoba)}, //Popoto Soba
            //{27871, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.popotoesauGratin)}, //Popotoesau Gratin
            //{19818, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.porkKakuni)}, //Pork Kakuni
            //{19836, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.porkStew)}, //Pork Stew
            //{16715, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.priestlyOmelette)}, //Priestly Omelette
            //{4740, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.princessPudding)}, //Princess Pudding
            //{36070, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pumpkinPotage)}, //Pumpkin Potage
            //{36069, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.pumpkinRatatouille)}, //Pumpkin Ratatouille
            //{27854, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.purpleCarrotJuice)}, //Purple Carrot Juice
            //{6959, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.candyDrop)}, //Purple Drop
            //{4708, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rabbitPie)}, //Rabbit Pie
            //{4728, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.raisins)}, //Raisins
            //{4692, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.raptorStew)}, //Raptor Stew
            //{27891, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rareRoastBeef)}, //Rare Roast Beef
            //{4677, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.ratatouille)}, //Ratatouille
            //{4667, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rawOyster)}, //Raw Oyster
            //{6956, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.candyDrop)}, //Red Drop
            //{27873, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.risottoalNero)}, //Risottoal Nero
            //{7572, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.roastCanard)}, //Roast Canard
            //{4649, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.roastDodo)}, //Roast Dodo
            //{27867, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.roastOvim)}, //Roast Ovim
            //{4683, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.roastedNopales)}, //Roasted Nopales
            //{27886, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.robeLettuceSalad)}, //Robe Lettuce Salad
            //{4725, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rolanberryCheesecake)}, //Rolanberry Cheesecake
            //{4727, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rolanberryLassi)}, //Rolanberry Lassi
            //{15425, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rolanberryShavedIce)}, //Rolanberry Shaved Ice
            //{24274, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.rooibosTea)}, //Rooibos Tea
            //{4706, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.roostBiscuit)}, //Roost Biscuit
            //{12859, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.royalEggs)}, //Royal Eggs
            //{7573, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sachertorte)}, //Sachertorte
            //{4662, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.salmonMeuniere)}, //Salmon Meuniere
            //{24279, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.salmonMuffin)}, //Salmon Muffin
            //{4671, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.saltCodPuffs)}, //Salt Cod Puffs
            //{4666, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.saltCod)}, //Salt Cod
            //{36056, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.saltedThavnairianCod)}, //Salted Thavnairian Cod
            //{4686, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sauerkraut)}, //Sauerkraut
            //{29501, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sausageandSauerkraut)}, //Sausageand Sauerkraut
            //{27866, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sausageLinks)}, //Sausage Links
            //{4648, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sauteedCoeurl)}, //Sauteed Coeurl
            //{22437, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sauteedGreenLeeks)}, //Sauteed Green Leeks
            //{12853, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sauteedPorcini)}, //Sauteed Porcini
            //{36076, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.scallopCurry)}, //Scallop Curry
            //{36075, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.scallopSalad)}, //Scallop Salad
            //{4654, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.scrambledEggs)}, //Scrambled Eggs
            //{12865, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.seafoodStew)}, //Seafood Stew
            //{21087, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sermonworthyMeuniere)}, //Sermonworthy Meuniere
            //{13743, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sesameCookie)}, //Sesame Cookie
            //{24280, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.shakshouka)}, //Shakshouka
            //{9333, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sharkFinSoup)}, //Shark Fin Soup
            //{4710, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.shepherdsPie)}, //Shepherds Pie
            //{19817, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.shorlog)}, //Shorlog
            //{36061, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sideritisCookie)}, //Sideritis Cookie
            //{29506, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.silkiePudding)}, //Silkie Pudding
            //{36052, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.skyr)}, //Skyr
            //{31901, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.smokedChicken)}, //Smoked Chicken
            //{4645, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.smokedRaptor)}, //Smoked Raptor
            //{4729, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.snowflakePeak)}, //Snowflake Peak
            //{12870, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.snurbleberryTart)}, //Snurbleberry Tart
            //{12846, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sohmAlTart)}, //Sohm Al Tart
            //{27857, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spaghettialNero)}, //Spaghettial Nero
            //{16711, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spaghettiCarbonara)}, //Spaghetti Carbonara
            //{16713, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spaghettiPescatore)}, //Spaghetti Pescatore
            //{14134, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spicedCider)}, //Spiced Cider
            //{36050, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spicyShakshouka)}, //Spicy Shakshouka
            //{7575, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spinachQuiche)}, //Spinach Quiche
            //{4684, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.spinachSaute)}, //Spinach Saute
            //{22431, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sprigganChocolate)}, //Spriggan Chocolate
            //{4742, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.starlightLog)}, //Starlight Log
            //{7570, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.steamedCatfish)}, //Steamed Catfish
            //{19824, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.steamedGrouper)}, //Steamed Grouper
            //{13742, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.steamedStaff)}, //Steamed Staff
            //{19832, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.steppeSalad)}, //Steppe Salad
            //{19810, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.steppeTea)}, //Steppe Tea
            //{19827, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stewedRiverBream)}, //Stewed River Bream
            //{4717, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stoneSoup)}, //Stone Soup
            //{4687, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stuffedArtichoke)}, //Stuffed Artichoke
            //{12866, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stuffedCabbageRolls)}, //Stuffed Cabbage Rolls
            //{4664, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stuffedCabbage)}, //Stuffed Cabbage
            //{12868, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stuffedChysahl)}, //Stuffed Chysahl
            //{29502, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.stuffedHighlandCabbage)}, //Stuffed Highland Cabbage
            //{38263, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sunsetCarrotNibbles)}, //Sunset Carrot Nibbles
            //{19825, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sweetandSourFrogsLegs)}, //SweetandSour Frogs Legs
            //{14141, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sweetGnomefish)}, //Sweet Gnomefish
            //{4744, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sweetRiceCake)}, //Sweet Rice Cake
            //{36066, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sykonBavarois)}, //Sykon Bavarois
            //{36065, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sykonCompote)}, //Sykon Compote
            //{36073, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sykonCookie)}, //Sykon Cookie
            //{36064, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.sykonSalad)}, //Sykon Salad
            //{9516, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tailormadeEelPie)}, //Tailormade Eel Pie
            //{22432, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.takoyaki)}, //Takoyaki
            //{19826, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tempuraPlatter)}, //Tempura Platter
            //{36074, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.thavnairianChai)}, //Thavnairian Chai
            //{36058, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.noodlesOfElpis)}, //Noodles Of Elpis
            //{22445, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tofuPancakes)}, //Tofu Pancakes
            //{4714, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tomatoPie)}, //Tomato Pie
            //{4715, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.trappersQuiche)}, //Trappers Quiche
            //{4658, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.treeToadLegs)}, //Tree Toad Legs
            //{9330, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tripleCreamCoffee)}, //Triple Cream Coffee
            //{36060, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tsaitouVounou)}, //Tsaitou Vounou
            //{4670, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.tunaMiqabob)}, //Tuna Miqabob
            //{31905, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.twilightPopotoSalad)}, //Twilight Popoto Salad
            //{36042, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.ukha)}, //Ukha
            //{12857, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.urchinLoaf)}, //Urchin Loaf
            //{38269, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.urchinPasta)}, //Urchin Pasta
            //{4697, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.walnutBread)}, //Walnut Bread
            //{19837, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.warriorsStew)}, //Warriors Stew
            //{4737, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.whiteChocolate)}, //White Chocolate
            //{6960, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.candyDrop)}, //White Drop
            //{16712, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.wildwoodScrambledEggs)}, //Wildwood Scrambled Eggs
            //{36044, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.winedarkSoup)}, //Winedark Soup
            //{36054, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.yakowMoussaka)}, //Yakow Moussaka
            //{4743, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.yearsOldPumpkinCookie)}, //Years Old Pumpkin Cookie
            //{6962, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.candyDrop)}, //Yellow Drop
            //{31904, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.zefir)}, //Zefir
            //{4680, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.zoni)}, //Zoni
            //{36037, new Consumable(FoodType., FoodTemp., Localization.lang.consumables.zurek)}, //Zurek
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
