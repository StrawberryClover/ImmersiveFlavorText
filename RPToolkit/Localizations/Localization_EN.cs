using Lumina.Excel.GeneratedSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Localizations
{
    internal class Localization_EN : LocalizationBase
    {
        public override TemperatureStages temperatureStages => new TemperatureStages(
            heatwave: new TemperatureDescription(
                increaseDesc: new string[]{
                    "You don't know how much more of this heat you can take, it's unbearable, what you wouldn't do to find something to help cool you off.",
                    "You can hardly endure this scorching heat any longer, it's intolerable, and you would do anything to find some respite and cool down.",
                    "This heat is sweltering."
                },
                decreaseDesc: new string[]{
                    "Nobody should see this message."
                }),
            veryHot: new TemperatureDescription(
                increaseDesc: new string[]{
                    "You start to feel a bit hotter now, very hot in fact.",
                    "You begin to feel quite overheated."
                },
                decreaseDesc: new string[]{
                    "The heat begins to feel more bearable.",
                    "The intensity of the heat starts to lessen, making it more tolerable."
                }),
            hot: new TemperatureDescription(
                increaseDesc: new string[]{
                    "You start to feel hot.",
                    "You start to experience discomfort from the rising heat, and you long for something that could offer respite and help you cool down."
                },
                decreaseDesc: new string[]{
                    "You take a sigh of relief as things aren't as hot as they were before, but it would be nice to cool off a bit more.",
                    "You feel the relief as the temperature subsides slightly from its previous intensity."
                }),
            roomTemp: new TemperatureDescription(
                increaseDesc: new string[]{
                    "It feels like the perfect temperature right now.",
                    "The current temperature seems to be just right, creating a pleasant sensation.",
                    "As the temperature climbs, it reaches an ideal level of warmth."
                },
                decreaseDesc: new string[]{
                    "It feels like the perfect temperature right now.",
                    "The current temperature seems to be just right, creating a pleasant sensation.",
                    "As the temperature begins to decrease, it reaches an optimal level of coolness."
                }),
            mild: new TemperatureDescription(
                increaseDesc: new string[]{
                    "You start to feel a bit more warm.",
                    "The temperature rises to a moderate level."
                },
                decreaseDesc: new string[]{
                    "The temperature seems to be cooling off a bit, things are starting to just feel just slightly warm now.",
                    "The temperature lowers to a mild degree."
                }),
            lukewarm: new TemperatureDescription(
                increaseDesc: new string[]{
                    "Things seem to be warming up a bit, now feeling lukewarm to you.",
                    "You start to warm up, seeming to be at a lukewarm level."
                },
                decreaseDesc: new string[]{
                    "It starts to feel lukewarm as the temperature cools down.",
                    "You start to cool off, seeming to be at a lukewarm level."
                }),
            chilled: new TemperatureDescription(
                increaseDesc: new string[]{
                    "The cold is not as bad as before, you begin to feel just a bit chilled now.",
                    "The cold becomes more tolerable compared to before, and you only seem to feel slightly chilled.",
                    "The temperature increases, and it reaches a chilly level."
                },
                decreaseDesc: new string[]{
                    "You start to feel chilly, things are starting to cool off quite a bit now.",
                    "You begin to feel a chill in the air, as things are noticeably cooling down."
                }),
            cold: new TemperatureDescription(
                increaseDesc: new string[]{
                    "You no longer feel so cold. You haven't stopped shivering, but it almost feels bearable now.",
                    "You start to feel a bit less cold, it's becoming more tolerable than before.",
                    "You are starting to feel less cold, though you still feel a chill, but not as much as before."
                },
                decreaseDesc: new string[]{
                    "You begin to feel quite cold, and start to shiver.",
                    "You begin to sense the chill, feeling the cold creeping in."
                }),
            veryCold: new TemperatureDescription(
                increaseDesc: new string[]{
                    "The air seems to lose it's frigid quality to it, but the ice cold bite in the air is not gone. Any chance you get you find yourself warming your hands in your pockets.",
                    "The air is no longer freezing, but remains bitterly cold."
                },
                decreaseDesc: new string[]{
                    "You feel like you can't bear the cold anymore, your whole body is shivering uncontrollably. It would be really nice to warm yourself by a campfire right about now.",
                    "You're beginning to feel extremely cold, with the chill penetrating through your body."
                }),
            frigid: new TemperatureDescription(
                increaseDesc: new string[]{
                    "Nobody should see this message."
                },
                decreaseDesc: new string[]{
                    "The air feels absolutely frigid, at this rate you feel like you are going to freeze.",
                    "You start to feel bitterly cold.",
                    "It is absolutely freezing. Any exposed skin starts to feel numb."
                })
            );

        public override rstring rainStarts => new string[] {
            "Gentle raindrops begin to fall upon your skin.",
            "You begin to feel a trickle of rain.",
            "Small rain drops start to fall upon your head."
        };
        public override rstring rainEnds => "The rain begins to clear.";

        public override BaseConsumableDescriptions consumables => new ConsumableDescriptions_EN();

        internal class ConsumableDescriptions_EN : BaseConsumableDescriptions
        {
            public override rstring steak => "As you take a bite of the <temp> <food>, you taste the rich and savory flavor of the steak, with a slightly crispy exterior and a juicy, tender interior.";
            public override rstring omelette => "As you take a bite of the fluffy <food>, you taste the <temp> and savory egg mixed with a hint of salt and pepper.";
            public override rstring chocolate => "As you eat the <food>, you taste the rich and creamy sweetness of the chocolate melting in your mouth, with hints of sweetness that leaves you with a lingering pleasant aftertaste.";
            public override rstring whiteChocolate => "";
            public override rstring candyDrop => "As you pop the shiny spherical <food> candy into your mouth, you feel a hard surface that quickly gives way to a chewy texture. The flavor bursts in your mouth, with a fruity sweetness that lingers on your tongue.";
            public override rstring grilledMeat => "As you sink your teeth into the succulent meat, you feel the savory flavors explode in your mouth, complemented by the garlicky undertones and a hint of salt. The texture is tender, juicy, and full of flavor, with a smoky aroma from the grill.";
            public override rstring grilledFish => "As you take a bite of the <food>, you savor the crispy exterior giving way to tender, juicy flesh. The natural flavors of the fish are enhanced by the subtle smokiness imparted by the grilling process, with just the right amount of salt bringing out its innate taste. The texture is both firm and flaky, offering a satisfying bite.";




            public override rstring acornCookie => "As you take a bite of the acorn cookie, you taste a crunchy, nutty flavor with a hint of sweetness. The cookie has a crisp texture and is slightly dry, but still enjoyable. There's a unique flavor to it that's difficult to place, but it's tasty nonetheless.";
            public override rstring alligatorSalad => "As you eat the <food>, you taste a refreshing combination of the sweet, juicy alligator pear slices and the tangy, slightly bitter dark vinegar, creating a satisfying balance of flavors.";
            public override rstring almondCreamCroissant => "As you take a bite of the almond cream croissant, you taste the buttery, flaky layers of the pastry as they melt in your mouth, complemented by the smooth, sweet creaminess of the almond paste.";
            public override rstring amraLassi => "As you drink amra lassi, a refreshing sensation washes over you, tasting the tanginess of the yoghurt which blends well with the sweet and mellow taste of amra fruit. The spices used in the drink add a subtle kick that enhances the overall flavor.";
            public override rstring amraSalad => "As you eat the salad, you experience a mix of sweet and savory flavors from the fresh fruit and the light dressing, which combine to create a refreshing and satisfying taste.";
            public override rstring anglerStew => "As you eat the stew, you taste the rich creaminess of the broth and the tender, flavorful chunks of fish, along with the savory notes of the onions and the herbal hint of chives.";
            public override rstring antelopeStew => "As you eat the antelope stew, the tender chunks of meat and the rich flavors of the wild vegetables blend together in a warm and comforting broth.";
            public override rstring appleJuice => "As you take a sip of the apple juice, you immediately taste the sweet and tangy notes of the freshly squeezed apples. The juice is cool and refreshing, and the crisp flavor lingers on your tongue.";
            public override rstring appleStrudel => "As you take a bite of the apple strudel, you taste the sweet tanginess of the apples mixed with the richness of the birch syrup, all encased in a delicate, buttery crust. The faint hint of cinnamon adds a subtle warmth to the overall flavor.";
            public override rstring appleTart => "As you take a bite of the apple tart, the sweet and slightly tart flavor of the apples blends perfectly with the aromatic spices, creating a delightful taste sensation. All wrapped inside of a buttery, flaky pastry crust, that breaks off in satisfying layers as you chew on it.";
            public override rstring archonBurger => "As you take a bite of the Archon Burger, you can feel the tender juiciness of the meat, the softness of the bun, the melting creaminess of the cheese, and the burst of flavors from the toppings and sauce, all combining to create a satisfying and mouth-watering taste.";
            public override rstring archonLoaf => "You slowly take a bite of the <food>, while the fluffy texture of the rye bread is noticeable, you are immediately overwhelmed by the taste. It's quite unappetizing, and is hard to describe. It accompanies a strong and lingering aftertaste, but hey at least it's 'healthy', right? You should find something to wash it down.";
            public override rstring arrosNegre => "You eat a bowl of arros negre, the savory and briny taste of the tender mussels blends perfectly with the buttery rice, all while the rich flavor of the squid ink, combined with the herbs and spices, adds a unique depth to the dish.";
            public override rstring baconBread => "As you take a bite of the bacon bread, you feel the softness of the dough and the chewiness of the thick-sliced bacon, and see the pattern of the bread twisted into a sheaf of wheat. The texture is satisfyingly dense, but not too heavy, and the taste is a perfect balance of savory bacon and freshly baked bread.";
            public override rstring baconBroth => "As you taste the bacon broth, you experience a savory and smoky flavor that coats your tongue, accompanied by a rich and slightly oily texture. The broth appears thin and translucent, with visible bits of shredded bacon and herbs floating within it.";
            public override rstring baguette => "As you bite into the crispy crust of the baguette, you feel the soft, airy texture of its interior. Its golden brown color and elongated shape add to its classic French appearance. The aroma of upland wheat is distinct and its taste is hearty and wholesome.";
            public override rstring bakedMegapiranha => "As you take a bite of the baked megapiranha, you feel its firm, flaky texture melt in your mouth. The outer layer is crispy, while the inside is moist and tender. The mild, delicate flavor of the fish is complemented by the rich, buttery sauce.";
            public override rstring bakedOnionSoup => "As you take a spoonful of the <food>, you feel the warmth and smoothness of the silky broth with tender onion chunks. The golden-brown cheese on top provides a crispy texture that perfectly complements the softness of the soup. The soup has a savory and rich flavor, with the onions adding a slight sweetness to it.";
            public override rstring bakedPipiraPira => "As you take a bite of the baked pipira pira, the tender flesh of the fish falls apart with ease, revealing the rich, buttery flavor infused with the earthy aroma of porcini mushrooms. The crispy outer layer of the leaf creates a satisfying crunch that contrasts with the soft, succulent texture of the fish, while the vibrant green color of the leaf adds a touch of visual appeal.";
            public override rstring bakedSole => "As you take a bite of the black sole, you can feel its delicate and flaky texture melt in your mouth, while the sauce adds a savory and slightly tangy flavor that complements the fish perfectly. The dish has a sophisticated appearance, with the lightly browned surface of the fish contrasting with the dark hue of the gil bun sauce.";
            public override rstring baklava => "As you take a bite of your baklava, the crunchy and flaky layers of phyllo dough and chopped nuts crumble and dissolve in your mouth, blending with the sweet and sticky honey syrup to create a rich and nutty flavor with a hint of sweetness. The pastry is delicately layered and visually appealing with a golden brown exterior and a nutty, honeyed filling.";
            public override rstring banhXeo => "As you take a bite of <food>, the crispy fried batter gives way to a soft and savory filling of meat and vegetables. The texture is crunchy on the outside and tender on the inside, while the taste is a combination of salty, sweet, and umami flavors. The appearance of the dish is golden and crispy on the outside with the filling peeking through.";
            public override rstring batteredFish => "As you take a bite of the crispy and golden battered fish, you feel the satisfying crunch of the batter coating, followed by the tender and flaky texture of the fish. The thick cut popotoes are crispy on the outside and fluffy on the inside. The addition of a dash of malt vinegar enhances the savory and slightly salty flavor.";
            public override rstring beefStew => "You feel the warmth of the stew emanating from the bowl, its dark color and chunks of buffalo meat visible amidst the wild onions. The meat is tender and practically melts in your mouth, while the onions add a slight crunch. The rich aroma of red wine fills your senses, and the complex flavors of the wine and buffalo meat blend together in a savory and satisfying taste.";
            public override rstring beefStroganoff => "You'll feel the warmth of the dish as you take a bite of the tender beef, and enjoy the creamy texture of the sauce as it coats your tongue. The stroganoff will appear rich and hearty, with chunks of popotoes and beef mixed in with the sauce. The taste will be savory, with a hint of spiciness from the spices and the slight bitterness of lettuce, providing a well-rounded flavor.";
            public override rstring beetSoup => "As you take a spoonful of this warm beet soup, you can feel the tender chunks of lean meat and soft beets in your mouth, accompanied by the crunchiness of sprouts. The soup has a smooth velvety texture, and the aroma of dill adds a touch of freshness to the dish. The flavor is rich and savory, with a slight sweetness from the beets that complements the meaty taste.";
            public override rstring crownedPie => "You can't help but feel a sense of whimsy and delight as you reach the plastic crown on top of the pie. The sweet aroma of cinnamon and syrup wafts up to your nose as you take a bite of the <food>. The crust is flaky and crisp, giving way to a soft and tender filling of warm apples and crunchy almonds, with the syrup adding a touch of sweetness and stickiness to the texture.";
            public override rstring blackTruffleRisotto => "As you take a bite of the black truffle risotto, you feel a smooth and creamy texture, while the small pieces of black truffle provide a slight crunch. The savory taste of the cheese and rice is enhanced by the pungent aroma of the black truffles, which adds a depth of earthiness. The diced onions also provide a subtle sweetness to balance the savory flavors.";
            public override rstring bloodCurrantTart => "As you take a bite of the <food>, the tangy taste of the red currants hits your palate, complemented by the sweetness of the syrup and sugar. The soft, buttery dough melts in your mouth, while the gelatin adds a slightly chewy texture. The vibrant red currants also add a pop of color to the tart.";
            public override rstring tomatoJuice => "The bright red color of the tomato juice immediately catches your eye. As you take a sip of the tomato juice, you feel the smooth texture of the juice coat your mouth. The texture is slightly thick and pulpy, with a subtle sweetness from the hint of honey and a tangy tartness from the lemon. The dominant taste is of fresh, ripe tomatoes, with a slightly acidic aftertaste.";
            public override rstring tomatoSalad => "As you take a bite of the salad, the crisp lettuce and juicy tomatoes create a refreshing crunch, while the tangy vinegar adds a subtle zing to the flavors without overwhelming them. The onions give a sharpness to the overall taste and texture of the dish.";
            public override rstring boiledAmberjackHead => "As you take a bite of the boiled amberjack head, you can feel the tender and succulent meat easily coming off the bones, and the soft texture of the boiled radish complementing the fish's flavor. The ginger and soy sauce seasonings give the dish a mild and savory taste, while the natural sweetness of the beet sugar adds a hint of sweetness.";
            public override rstring boiledBream => "As you take a bite of the boiled bream, the tender and flaky fish flakes apart in your mouth, while the garlic, onion, and olive oil lend a savory flavor to the dish. The eggplant and cabbage provide a satisfying crunch, adding a pleasant texture to the meal.";
            public override rstring boiledCrayfish => "As you peel the shell of the boiled crayfish, you feel the tender and moist flesh inside. The appearance of the crayfish is a bright red color with a curved body and a pair of sharp claws. The texture of the meat is firm and slightly chewy. As you take a bite, you taste a sweet and delicate flavor, with a hint of salt.";
            public override rstring boiledEgg => "As you take a bite of the boiled egg, you feel the smooth texture of the firm, cooked egg white against your tongue. The yolk inside is soft and creamy, giving way easily to your bite. The taste is simple and mild, with a subtle richness from the yolk that complements the neutral flavor of the egg white.";
            public override rstring borscht => "As you take a spoonful of the hearty borscht, you notice the deep red color of the soup with chunks of tender meat and vegetables. The texture is thick and slightly creamy, with a satisfying blend of sweet and savory flavors from the beets and meat, enhanced by the slight tang of cheese.";
            public override rstring boscaiola => "As you twirl the vermicelli noodles around your fork, you can see the rich tomato sauce clinging to them. The aroma of garlic and mushrooms wafts up to your nose as you take a bite, and the chewy texture of the noodles contrasts nicely with the tender mushrooms. The tomato sauce adds a slight tangy sweetness to the dish, making each bite a harmonious blend of savory and sweet.";
            public override rstring bouillabaisse => "As you take a spoonful of the <food>, you notice the vibrant red color of the tomato-based broth and the tender pieces of shrimp and oyster. The broth is thick and creamy, and is seasoned with aromatic saffron. The texture is both smooth and slightly chunky, with the shrimp and oyster providing a pleasant chewiness. It is slightly oily, adding a subtle nutty flavor and a hint of richness.";
            public override rstring braisedPipira => "As you savor the braised freshwater fish, the flesh is tender and flaky with a subtle sweetness that pairs perfectly with the rich and earthy chanterelle mushrooms. The buttery sauce brings a velvety smoothness to the dish, while the fish's delicate texture contrasts with the meatiness of the mushrooms.";
            public override rstring broadBeanCurry => "As you eat the broad bean curry, you feel the creamy texture of the beans and rice melting in your mouth, and the warmth of the spices filling your palate. The dish has a rich yellow color from the curry, and the aroma of cardamom is enticing. The beans are tender and have a slight bite to them, while the rice is fluffy and flavorful.";
            public override rstring broadBeanSalad => "As you eat the broad bean salad, you feel the crisp texture of the lettuce contrasting with the softness of the broad beans, while the purple carrots add a touch of sweetness. The frantoio oil and parsley dressing provides a refreshing and tangy flavor, completing the dish with a balanced taste.";
            public override rstring broadBeanSoup => "As you eat the broad bean soup, you feel the warmth of the broth filling you up. The soup is thick and hearty, with tender chunks of rail tenderloin and soft broad beans. The broth is savory and flavorful, with a hint of sweetness from the beans. The texture of the soup is creamy and smooth, and the flavors are rich and comforting.";
            public override rstring buckwheatTea => "As you sip on buckwheat tea, you'll notice a rich, earthy flavor and aroma that fills your senses. The tea has a dark amber color and a smooth texture that coats your mouth, with a subtle sweetness that comes from the buckwheat kernels.";
            public override rstring butteryMogbiscuit => "As you take a bite of the buttery mogbiscuit, you feel the delicate crumble of the cookie as it melts in your mouth. Its golden-brown exterior gives way to a soft, moist center that is rich and buttery in flavor, leaving a lingering sweetness on your tongue.";
            public override rstring buttonMushroomSaute => "As you take a bite of the button mushroom sauté, you feel the soft texture of the mushrooms and taste the earthy flavor, complemented by the fragrant garlean garlic and sage, while the smooth olive oil adds a rich undertone to the dish.";
            public override rstring buttonsInABlanket => "As you take a bite of the buttons in a blanket, you experience a juicy burst of flavor from the tender button mushrooms that are wrapped in the soft, slightly sweet cabbage leaves. The savory sauce imparts a rich umami flavor that perfectly complements the mushroom's earthy taste, and the saffron adds a delicate floral note. The texture is a combination of soft and chewy, with the cabbage providing a slight crunch to every bite.";
            public override rstring calamariRipieni => "At first glance, you notice a plump and moist squid, cooked to a perfect tenderness and garnished with a rich tomato sauce. As you take a bite, the smooth and creamy cheese filling is the first sensation on your tongue, followed by the savory and slightly sweet flavors of the alien onion and blood tomato. The squid itself is soft and succulent, complementing the burst of flavors from the filling.";
            public override rstring caramels => "As you take a look at the caramels, you notice their shiny and smooth surface, with a deep amber color. As you take a bite, you feel the chewy texture and the buttery softness melting in your mouth, followed by a rich and creamy taste that is both sweet and slightly tangy, with a hint of caramelized sugar.";
            public override rstring carrotNibbles => "As you take a look at the carrot nibbles, you notice that they are a bright orange color and glisten with a light coating of dressing. As you take a bite, you feel the crunchiness of the carrots and their cool, refreshing texture. The taste is slightly sweet with a tangy flavor from the vinegar, and the thinly sliced cucumbers add a subtle freshness to the dish.";
            public override rstring carrotPudding => "As you take a look at the carrot pudding, you notice its smooth and creamy appearance. As you take a bite, you feel the coolness of the pudding on your tongue, and its silky texture as it melts in your mouth. The taste is a delightful blend of sweet and savory, with the natural sweetness of the carrots and sugars balanced by a subtle richness from the eggs and milk.";
            public override rstring cawlCennin => "As you gaze at the cawl cennin, you notice a creamy, light green color with small bits of brown and green herbs. As you take a spoonful, you feel the warmth of the soup and the smooth texture as it touches your tongue. The soup has a velvety consistency, with small bits of tender vegetables and a subtle hint of creaminess. The taste is savory and slightly sweet, with a mild onion flavor complemented by the richness of the chicken stock.";
            public override rstring chamomileTea => "As you take a look at the chamomile tea, you notice its pale yellow hue and clarity. As you take a sip, you feel its soothing warmth and smooth texture. The tea is mild and subtly sweet, with a gentle honey flavor that complements the floral notes of chamomile.";
            public override rstring chanterelleSaute => "As you gaze at the plate, you see golden-brown chanterelle mushrooms, slightly curled and glistening from the butter. You take a bite, feeling the delicate texture of the mushroom and the smooth richness of the melted butter. Every bite is warm and comforting, and the taste is earthy and savory, with a subtle nuttiness.";
            public override rstring charredCharr => "As you take a look at the charred heather charr, you notice its blackened exterior and the aroma of smoke. As you take a bite, you feel the tender and flaky texture of the fish, which is warm from being cooked over an open flame. The salt adds a slight crunch and enhances the natural flavors of the fish, while the hint of algae in the background provides a unique and savory taste.";
            public override rstring chawanmushi => "As you take a look, you see a smooth and creamy pudding with specks of parsley and shiitake mushroom. As you take a bite, you feel the warmth of the pudding and its smooth texture. The taste is savory and rich with the flavor of the broth and egg, followed by a subtle earthiness from the shiitake mushroom and a hint of parsley.";
            public override rstring cheeseRisotto => "As you lift your spoon, you notice the creamy texture and aroma of the cheese sauce coating the sticky rice. As you take a bite, the warmth of the risotto fills your mouth, the rice soft yet slightly firm, with chunks of savory parsnip and onion mixed throughout. The tangy and salty taste of the cottage cheese stands out, adding a delightful complexity to the risotto.";
            public override rstring cheeseSouffle => "As you set your eyes on the cheese souffle, you notice its golden-brown top that's perfectly puffed and risen. As you take a bite, your teeth sink into the airy texture, and you savor the creamy, cheesy taste with a hint of sweetness from the syrup and a subtle tang from the lemon. The souffle is warm and comforting, melting in your mouth, leaving a delicious aftertaste.";
            public override rstring chickenAndMushrooms => "As you glance at your plate, you see tender pieces of chicken breast and savory mushrooms glistening with butter. You feel the warmth emanating from the dish, inviting you to take a bite. The chicken is soft and juicy, while the mushrooms are meaty and earthy, creating a wonderful combination of flavors and textures in your mouth. The dish is hearty and comforting, perfect for a cozy night in.";
            public override rstring chickenFettuccine => "As you gaze upon your plate, you see a generous serving of chicken fettuccine, with the creamy sauce coating the thick noodles and pieces of chicken. As you take your first bite, you feel the satisfying chewiness of the pasta and the tender, juicy texture of the chicken. The warmth of the dish spreads through your mouth and down your throat, with a creamy and slightly minty taste that lingers on your tongue.";
            public override rstring chiliCrab => "As you gaze upon the chili crab, you notice the vibrant red hue of the blood tomato sauce encasing the blue crab in its shell. The shell's rough texture contrasts with the smooth and delicate meat inside. As you take a bite, the tender crab meat pairs perfectly with the spiciness of the sauce, while the subtle sweetness of the crab lingers on your tongue. The dish is served hot, and the warmth enhances the flavors, leaving you with a satisfying, savory aftertaste.";
            public override rstring chilledPopotoSoup => "As you gaze upon the chilled popoto soup, you can see its creamy, pale yellow color, speckled with bits of green parsley. As you take a spoonful, you feel the cool temperature and the smooth, velvety texture of the soup on your tongue. The taste is refreshing and savory, with a slight sweetness from the leeks and cream and a hint of herbaceousness from the parsley.";
            public override rstring chirashizushi => "With a colorful mix of ingredients arranged beautifully over a bed of sticky rice, the chirashi-zushi looks like a work of art. As you take a bite, the texture is tender and moist, with a slight crunch from the lotus root. The temperature is cool, perfect for a refreshing bite. The taste is a perfect balance of sweetness from the rice vinegar, the umami flavor of the shiitake mushrooms, the savory taste of the tiger prawn, and the subtle richness of the eggs.";
            public override rstring clamChowder => "As you take a spoonful of the clam chowder, you feel the warmth and creaminess of the soup filling your mouth. The texture is smooth and velvety, with tender pieces of clams adding a subtle chewiness to the soup. The taste is rich and savory, with a slight briny flavor from the clams that is perfectly balanced by the delicate sweetness of the milk-based broth.";
            public override rstring cockatriceMeatballs => "As you take a bite of the cockatrice meatballs, you feel their warm and juicy texture in your mouth. The meat is tender and savory, with a slight gamy flavor that is complemented by the tanginess of the tomato sauce. The meatballs are served on top of a bed of crispy flatbread that adds a crunchy element to the dish. The onions are caramelized to a golden brown, adding a subtle sweetness to the meatballs.";
            public override rstring coconutCodChowder => "As you dip your spoon into the Coconut Cod Chowder you feel the warmth of the steam rising to your face. The soup is creamy, smooth, and silky. Chunks of cod and bits of tomato and carrot provide a welcome and satisfying diversity in texture. The coconut milk has made it rich and slightly sweet, with a subtle kick from the hint of paprika. Truly the perfect dish for a chilly day - you are filled with warmth and comfort.";
            public override rstring coffeeBiscuit => "As you take a bite of the coffee biscuit, the texture is crispy on the outside and crumbly on the inside. It's warm from being freshly baked and the taste is buttery and sweet with a subtle coffee flavor. You can feel the crunch of the coffee beans in every bite, and the beet sugar adds a perfect touch of sweetness without being overwhelming.";
            public override rstring cornbread => "As you take a bite of the warm cornbread, you feel its crumbly texture melting in your mouth. The taste is slightly sweet and savory, with a hint of cornmeal and a subtle tang from the buffalo milk. The bread is warm and comforting, with a golden brown crust on the outside and a soft, fluffy interior. Its aroma is reminiscent of freshly baked bread, with a slight hint of olive oil. You can taste the nutty flavor of the wheat flour, which balances out the sweetness of the cornmeal.";
            public override rstring crabCakes => "As you take a bite of the crab cakes, the crispy exterior gives way to a tender and moist center that melts in your mouth. The sweet and delicate taste of the blue crab meat is perfectly complemented by the subtle flavors of wheat flour and onions. Served warm, the texture is just right, not too hard or too soft, with a slight crumbly feel that adds to the overall experience. Each bite is a perfect combination of flavors and textures, making it difficult to resist taking another.";
            public override rstring crabCroquette => "As you take a bite of the crab croquette, you can feel the crispy exterior crunch between your teeth, giving way to the smooth, creamy filling inside. The sweet, succulent flavor of the crab is complemented by the subtle hints of onion and popoto, all wrapped up in a warm, comforting texture. The croquette is served warm, and the indulgent richness of the dish is perfectly balanced by the sweet sauce.";
            public override rstring creamySalmonPasta => "As you twirl the vermicelli noodles around your fork, the creamy salmon pasta oozes with every bite. The dish is warm and comforting, and the flaky salmon adds a hint of smoky flavor to the cream sauce. The frantoio oil and pepper give a gentle kick of heat that balances well with the sweetness of the cream. The spinach adds a pop of green color and a slight bitterness to the overall flavor.";
            public override rstring cremeBrulee => "As you indulge in the creme brulee, the first thing you notice is the satisfying crunch of the caramelized sugar topping. Underneath, the creamy custard is cool and silky smooth on your tongue. The vanilla flavor is delicate yet prominent, and the sweetness of the birch syrup adds a unique depth of flavor. The overall texture is velvety and luxurious, making each spoonful feel like a decadent treat.";
            public override rstring crimsonCider => "As you take a sip of the crimson cider, you feel a warming sensation flow down your throat. The sweet and slightly tangy taste of the loquat is complemented by the spiciness of the crimson pepper and ginger. The cumin adds a subtle earthy flavor that balances the sweetness of the cider. The kudzu root gives the cider a slightly thick texture, making it more filling than regular cider. It's the perfect drink for a chilly evening.";
            public override rstring crumpet => "As you take a bite of the crumpet, you notice its soft, spongy texture that immediately melts in your mouth. The warm, buttery flavor is complemented by the sweetness of the syrup, and you savor the taste as you chew. It's a perfect accompaniment to a cup of hot tea. The crumpet's simplicity makes it a comforting and satisfying treat, and you can understand why it's a beloved snack in Ul'dah.";
            public override rstring daggerSoup => "As you take a sip of the warm dagger soup, you immediately notice the delicate balance of flavors. The broth has a slightly salty taste, which perfectly complements the tender chunks of navigator's dagger and cloud cutter. The leeks add a hint of sweetness to the dish, while the meatballs made from ground-up fish paste give it a satisfying texture. It feels comforting and warming, making it the perfect meal for a chilly day.";
            public override rstring darkPretzel => "As you bite into the dark pretzel, you feel the doughy texture give way to a satisfying crunch. The taste is a perfect blend of salty and sweet, with hints of butter and maple sugar throughout. The bread is warm and comforting in your mouth, making it an excellent snack for any time of day.";
            public override rstring deepfriedOkeanis => "As you take a bite of the deep-fried okeanis, you notice the crispy outer layer immediately giving way to the tender, flaky meat inside. Despite its unusual appearance, the taste is surprisingly mild and slightly sweet, with a subtle hint of salt. The texture is a combination of soft and crispy, making for a delightful contrast with every bite. The dish is served hot, with steam rising from the golden-brown crust.";
            public override rstring deviledEggs => "As you take a bite of the deviled eggs, you experience a creamy, smooth texture from the filling, which contrasts with the firmness of the boiled egg white. The flavor is rich and slightly tangy, with a hint of saltiness from the sardine paste and a pleasant zing from the mustard and lemon. The paprika and parsley add a touch of earthiness and freshness, respectively.";
            public override rstring dhalmelFricassee => "As you take a bite of the dhalmel fricasse, you feel the tender and juicy meat melt in your mouth. The savory flavor of the meat blends perfectly with the rich, creamy taste of the sour cream. The onions add a slight sweetness while the laurel gives it a subtle, earthy aroma.";
            public override rstring dhalmelGratin => "As you take a bite of the dhalmel gratin, you feel the creamy texture of the sauce coating your tongue. The thinly sliced dhalmel meat and vegetables have a tender and melt-in-your-mouth texture. The dish is savory with a hint of sweetness from the onions and zucchini, and the sour cream adds a tangy flavor to the mix. The gratin is warm from the oven and has a slightly crispy top layer.";
            public override rstring domanTea => "As you take a sip of Doman tea, you immediately feel a warm and comforting sensation. The sweet aroma of the dried persimmons mixed with the spiciness of cinnamon and ginger hits your nose. The tea has a smooth and silky texture, with a light, fruity taste that is not too overpowering. The beet sugar adds a nice touch of sweetness that perfectly balances the flavors.";
            public override rstring driedPlums => "As you take a bite of the dried plum, you immediately notice the chewy texture and the slight stickiness it leaves in your mouth. The fruit is sweet with a slightly tangy flavor, and you can taste the sun-dried sweetness of the plum. Despite being dried, it is still moist and succulent, and the flavor lingers on your tongue long after you've finished chewing.";
            public override rstring dzemaelGratin => "As you take a bite of the <food>, you feel the soft and tender texture of the popoto melting in your mouth, along with the slight crunch of the baked eft tail. The combination of garlic, pepper, and nutmeg gives it a savory and slightly spicy taste, perfectly balanced by the sweetness of the cream.";
            public override rstring eelPie => "What may seem like an odd pie at first, as you bite into the eel pie, the rich and savory flavor of the black eel mixed with the butter, pepper, and cloves immediately hits your taste buds. The pie crust adds a satisfying crunch to the otherwise soft and tender filling. Despite its appearance, the eel is surprisingly delicious and complements the buttery crust.";
            public override rstring eggFooYoung => "As you take a bite of egg foo young, you are immediately hit with the satisfying texture of the fluffy omelette. The combination of crab, kudzu root, shiitake mushrooms, bamboo shoot, and fish stock provide a delicious mix of flavors that blend perfectly with the egg. The savory sauce on top adds a final touch that ties everything together.";
            public override rstring elpisDeipnon => "As you take a bite of <food>, you feel the firm texture of the seared bird breast, perfectly balanced with the softness of the onion and tomato. The flavor of the dark rye and wheat blend together to create a subtle nuttiness, while the sea salt brings out the natural savory taste of the meat. Overall, the dish is satisfying and filling, making it a popular choice for a hearty meal.";
            public override rstring emaDatshi => "As you take a bite of ema datshi, you immediately feel the fiery heat from the crimson and dragon peppers. The creamy cheese base helps to tame the spice, but it's still quite intense. The peppers have a slight crunch, while the cheese is soft and gooey. The dish has a tangy and slightly sour taste, likely from the fermentation of the peppers. You can feel your taste buds come alive with the complex mix of flavors.";
            public override rstring emeraldSoup => "As you take a spoonful of the emerald soup, you feel a thick and slightly gritty texture in your mouth. The taste is earthy and savory, with a subtle bitterness that lingers on your tongue. The soup is warm and comforting, perfect for a cool day. You can taste the rich flavors of the buffalo beans and the fragrant chives, which give the soup a hint of freshness.";
            public override rstring espressoConPanna => new string[]{
                "As you take a sip of the espresso, the smooth texture and bold taste of the coffee hit your palate first. The rich, full-bodied flavor is then perfectly balanced by the creamy tang of the sour cream, which also gives the drink a slightly thicker consistency. The sweetness of the beet sugar and the subtle warmth of the cinnamon provide a perfect finish.",
                "As you take a sip of the espresso, you initially taste the rich, bold flavors of the coffee, but soon after, you are hit with a tangy, creamy taste of the sour cream. The two flavors blend together in a surprisingly harmonious way, leaving you with a unique and satisfying taste in your mouth."
            };
            public override rstring exquisiteBeefStew => "As you take a spoonful of this exquisite beef stew, the tender dzo tenderloin melts in your mouth and the hearty popotoes and carrots provide a satisfying crunch. The thick, comforting broth is infused with rich flavors from the simmering process, making it the perfect dish to warm you up on a chilly day.";
            public override rstring farmersBreakfast => "As you dig into the farmer's breakfast, the rich aroma of sizzling eggs and popotoes fills your senses. The soft, fluffy eggs with silky yolks and chunks of tender ovim meat create a comforting texture, while the crispy popotoes provide a satisfying crunch. The dish is finished off with the subtle herbaceous notes of curly parsley and the mild tang of frantoio oil.";
            public override rstring figBavarois => "As you take a bite of the fig bavarois, your taste buds are greeted with a burst of sweetness that is not overpowering. The gelatinous texture of the dessert is smooth and delicate, making it easy to savor. The flavors of the fig and the subtle sweetness of the maple sugar complement each other perfectly. The richness of the yak milk gives the dessert a creamy consistency, which melts in your mouth, leaving a pleasant aftertaste.";
            public override rstring fingerSandwich => "As you bite into the warm, toasted walnut bread, you're met with the creamy, smooth texture of a perfectly cooked egg. The fresh crunch of lettuce adds a refreshing element, while the buttery richness of the spread brings it all together. It's a satisfying and classic sandwich, perfect for a quick breakfast or lunch on the go.";
            public override rstring fishSoup => "As you bring the spoon to your lips, you are greeted with a warm and comforting aroma of fish and herbs. As you take a sip, the soup feels light on your tongue, yet is packed with flavor. You can taste the subtle sweetness of the bream fish, which is perfectly balanced by the tanginess of the tomatoes and the freshness of the parsley. The smooth texture of the soup is complemented by the buttery richness, while the hint of nutmeg adds a pleasant, earthy note. The vegetables add a nice crunch and variety, making every bite a delight.";
            public override rstring fishStew => "As you take a spoonful of the fish stew, you feel the soft and chewy texture of the balls of fish paste mixed with the slightly firm popotoes. The clam adds a slight chewiness to the dish. The broth has a salty taste that complements the flavor of the seafood, and the subtle flavor of green leek adds a hint of freshness to the dish. The dish is well-balanced and has a complex taste that reflects the coastal cuisine of the Far East.";
            public override rstring flatbread => "As you take a bite of the flatbread, you notice its slightly dense texture and a faint nuttiness from the rye flour. The bread is not overly moist nor dry, striking a perfect balance in your mouth. The flavor is subtle yet distinct, with a slight hint of salt adding just the right amount of seasoning to the bread.";
            public override rstring flaugnarde => "As you take a bite of the flaugnarde, you notice the soft and creamy texture of the baked pudding. The sweetness of the apples mixed with the birch syrup is complemented by the richness of the sweet milk and cream. The egg and flour in the mixture provide a smooth and silky consistency that melts in your mouth. You can taste the warm spices that add a comforting aroma to the dessert.";
            public override rstring flintCaviar => "As you savor the delicacy, you notice the small black pearls of flint caviare pop in your mouth, releasing a salty and savory taste. The texture is smooth and almost buttery, with a slight firmness that adds to the sensation of the eggs bursting in your mouth. The flavor is rich and decadent, with a distinct umami taste that lingers on your palate. You feel a sense of luxury and indulgence.";
            public override rstring forestMiqabob => "As you take a bite of the forest miq'abob, the button mushrooms offer a juicy and chewy texture with a slight earthy taste. The ruby tomato bursts with a sweet and acidic flavor, contrasting with the savory mushrooms. As you bite into the next mushroom, you feel the tender and meaty texture. The aloe adds a crunchy and refreshing bite, complementing the other ingredients with its mild and slightly sweet flavor. All of the ingredients are coated in lavender oil, adding a subtle floral aroma and taste, while the salt and pepper enhance the natural flavors of the kabob.";
            public override rstring friedEgg => "As you cut into the sunny-side up fried egg, the bright yellow yolk spills out onto your plate. The egg white is firm but not rubbery, and has a slightly crispy edge where it was in contact with the hot butter in the pan. As you take a bite, the rich, buttery flavor of the yolk and the slight saltiness from the seasoning hit your taste buds. The combination of textures - the silky yolk and the firm white - creates a satisfying mouthfeel.";
            public override rstring frozenSpirits => "As you take a sip of the frozen spirits, you immediately feel the cold, icy texture of the slushy drink. The flavors of the fermented cloud bananas and Old World figs meld together perfectly, creating a unique and delicious taste. The milk adds a subtle creaminess to the drink, which helps to balance out the sweetness of the fruit. As you continue to drink, you can taste the different layers of flavors, with the sweetness of the banana and fig coming through in each sip.";
            public override rstring frumenty => "As you take a bite of the frumenty, you are met with a warm and comforting feeling. The texture is smooth and creamy, with a slightly grainy consistency from the cracked wheat. The raisins provide a chewy sweetness that pairs well with the warm spices of cinnamon and honey. The aldgoat milk gives a rich and creamy flavor, tying everything together. It's the perfect thing for a cozy morning.";
            public override rstring futomakiRoll => "As you take a bite of the futo-maki roll, you experience a delightful combination of textures and flavors. The chewy seaweed wrap, soft vinegared rice, succulent spiny lobster, savory eel, earthy shiitake mushroom, and fluffy egg come together in a perfect harmony of tastes. You savor each bite.";
            public override rstring gameni => "With each spoonful of gameni, you experience a complex interplay of textures and flavors. The tenderloin is succulent and savory, while the vegetables provide a crunchy and earthy contrast. The shiitake mushrooms add a deep, rich umami flavor that complements the soy sauce broth. As you savor each bite, you notice the subtle sweetness from the lotus root and carrot that round everything out.";
            public override rstring garleanPizza => "As you take a bite of the Garlean pizza, the cheese stretches and melts in your mouth. The wheat crust is crispy on the outside and chewy on the inside, providing a satisfying texture. The sweet tomato sauce and fragrant basil give the pizza a burst of flavor, while the onions add a subtle crunch.";
            public override rstring giantHaddockDip => "As you taste the giant haddock dip, you feel the creamy texture of the mashed popoto mixed with the fish roe. The dip has a savory flavor with a slight tang from the lemon juice, while the perilla oil adds a hint of earthiness. The garlic and pepper give a subtle kick to the overall taste.";
            public override rstring giantPopotoPancakes => "As you take a bite of the pancake, the crispy exterior gives way to a fluffy and soft interior. The popoto flavor is prominent, with hints of sweetness from the golden honey and a slight nutmeg spice. The texture is satisfyingly soft yet substantial, making for a filling bite.";
            public override rstring gingerCookie => "As you take a bite of the ginger cookie, you feel the crunchy texture of the cookie itself and the soft, buttery texture of the ginger inside. The sweet and spicy taste of the maple sugar and ginger blend together perfectly, leaving a warm sensation in your mouth.";
            public override rstring gingerSalad => "As you take a bite of the ginger salad, you immediately notice the crunchy texture of the lettuce and radish. The sharp, spicy taste of the ginger is complemented by the subtle kick of pepper and the earthy flavor of olive oil. The combination of flavors and textures creates a refreshing and satisfying salad.";
            public override rstring gloryBeSoup => "As you take a sip of the Glory Be Soup, you feel the warmth spread through your body. The hearty stew is thick with lentils and chestnuts, each bite bursting with flavor. The perilla oil adds a unique taste and texture, while the pepper and peppermint provide a subtle kick. The soup is reminiscent of a cozy winter night by the fire, comforting and satisfying.";
            public override rstring goldenPineappleJuice => "As you take a sip of the pineapple juice, your taste buds are immediately awakened by the sweet, tangy flavor of the freshly squeezed pineapple. The honey adds a natural sweetness while the lemon juice adds a refreshing sourness that balances the flavors perfectly. The subtle notes of amber cloves provide a warm and comforting spice that makes this drink perfect for any time of the day. The texture is smooth and silky, making it easy to drink and leaving a pleasant aftertaste.";
            public override rstring grapeJuice => "As you take a sip of the grape juice, you are immediately hit with the sweet and tangy flavor of freshly squeezed grapes. The texture is smooth and velvety, with just the right amount of pulp to give it a satisfying mouthfeel. The overall taste is refreshing and delightful, leaving you feeling energized and rejuvenated.";
            public override rstring grilledCarp => "As you take a bite of the grilled carp, you feel the tender and moist flesh melt in your mouth, leaving behind a savory and slightly smoky flavor. The skin is crispy and adds a delightful texture to the dish. You can taste the natural sweetness of the fish, and the salt enhances the flavor without overpowering it.";
            public override rstring grilledDodo => "As you take a bite of the grilled dodo, the smoky flavor and aroma immediately hits your senses. The tenderloin is perfectly cooked, tender and juicy, and the garlic and salt give it just the right amount of seasoning without overpowering the natural taste of the meat. The texture is firm yet tender, with a slight crispness on the outside from the grilling. The overall taste is rich and savory, with a hint of sweetness from the natural flavor of the meat.";
            public override rstring grilledTurban => "As you bite into the <food>, the savory umami taste of the soy sauce and sake hits your palate, complementing the delicate and slightly chewy texture of the meat. The shell adds a subtle smoky flavor to the dish, making each bite more satisfying than the last. The aroma of the cooking wine enhances the overall experience, leaving you wanting more.";
            public override rstring gyros => "As you take a bite of the gyros, the tender and juicy loaghtan meat coated in sea salt delights your taste buds. The crisp lettuce and soft wheat bread contrast with the rich meat, creating a perfect blend of flavors and textures. With each bite, you savor the combination of savory and refreshing tastes.";
            public override rstring haddockDip => "As you scoop up a spoonful of haddock dip, the texture is creamy and smooth, with bits of flaky haddock mixed in. The taste is a balance of salty and savory, with a tangy kick of lemon and garlic. The popoto base adds a hearty depth to the dip.";
            public override rstring hamsaCurry => "As you take a bite of the hamsa curry, you immediately notice the rich and complex combination of flavors. The tender hamsa meat is infused with a mixture of nutmeg seeds and coconut milk, creating a creamy and aromatic base. The spice blend is a perfect balance of heat and sweetness, complemented by the slightly crunchy texture of the carrots. The fermented butter adds a depth of flavor and richness to the dish, making it a truly satisfying meal.";
            public override rstring happinessJuice => "As you take a sip of the happiness juice, you can feel the cool and refreshing mint flavor dancing on your tongue, followed by a burst of sweet carrot flavor. The texture of the drink is smooth and silky, with a slight thickness from the carrot pulp. Overall, it feels like a refreshing and healthy drink that satisfies your thirst and craving for something sweet.";
            public override rstring heavenlyEggnog => "As you take a sip of the heavenly eggnog, the rich and creamy texture coats your tongue, with a slight thickness that lingers in your mouth. The sweetness of the birch syrup and cream milk is balanced by the warmth of the cinnamon and nutmeg, giving it a complex and satisfying flavor profile. The egg gives it an added richness, making it feel almost like a dessert in a glass.";
            public override rstring heavenseggSoup => "As you take a spoonful of the heavensegg soup, the rich aroma of smoked bacon and onion hits your nose. The soup is silky smooth and the egg creates a creamy texture that coats your tongue. The savory and smoky flavors of the bouillon and bacon perfectly complement the slightly sweet taste of the pumpkin. It's a comforting and hearty dish that leaves you feeling satisfied.";
            public override rstring herringPie => "As you take a bite of the herring pie, you can't help but admire its fish-shaped appearance, with golden-brown crust and visible pieces of spinach and mushroom baked into it. The crust is flaky and crispy, while the herring inside is moist and savory with a hint of saltiness. The combination of flavors and textures is delightful, and you savor each bite.";
            public override rstring honeyBun => "As you take a bite of the honey bun, the first thing that strikes you is its golden brown color, which seems to glow in the light. The lightly glazed exterior gives way to a soft, buttery texture that's warm and comforting. The pastry has a slight sweetness, which is balanced by the richness of the butter and the slight tanginess of the dough. It's a perfect treat for those who enjoy a balance of sweet and savory flavors.";
            public override rstring honeyCroissant => "As you take a bite of the honey croissant, you are met with the satisfying crunch of its buttery layers, followed by the soft and pillowy texture of the pastry. The honey provides a subtle sweetness, balanced out by a refreshing hint of peppermint. The combination of the strong flour and yak milk gives the croissant a rich, hearty flavor that is both indulgent and satisfying.";
            public override rstring honeyMuffin => "As you take a bite of the honey muffin, you feel the moist and crumbly texture melt in your mouth, revealing a sweet and delicate flavor of honey. The cakey texture of the muffin is soft and airy, and the honey gives it a subtle richness that complements the lightness of the muffin. With each bite, you savor the perfect balance of sweetness and softness that leaves you feeling satisfied yet still craving for more.";
            public override rstring hotChocolate => "As you take a sip of the hot chocolate, you feel a wave of warmth spread through your body, and the rich aroma of kukuru fills your senses. The texture is thick and creamy, with a velvety smoothness that coats your tongue. The taste is a perfect balance of sweetness and bitterness, with the sweetness coming from the syrup and the bitterness from the kukuru powder. The drink is piping hot, with a decadent and indulgent experience that is perfect for satisfying your sweet cravings and warming you up on a cold day.";
            public override rstring hourglassBiscuit => "As you bite into the hourglass biscuit, you notice the crisp exterior giving way to a soft, nutty center. The subtle sweetness of the acorn blends perfectly with the rich, buttery flavor of the cookie, creating a unique and satisfying taste. The texture is light and crumbly, making it easy to devour one after another without feeling too full.";
            public override rstring imamBayildi => "As you take a bite of the imam bayildi, the explosion of flavors on your tongue is almost overwhelming. The eggplant is tender and juicy, while the mixture of marinated vegetables, onion, parsley, and spicy crimson pepper adds a fiery kick to the dish.";
            public override rstring ishgardianMuffin => "As you take a bite of the Ishgardian muffin, you feel the soft and slightly chewy texture of the bread. The combination of wheat and cornmeal gives it a subtle grainy texture that adds an interesting depth to the overall mouthfeel. The flavor is mild and comforting, with a subtle sweetness from the milk that is not overpowering. You can taste the nuttiness of the wheat and the slight earthiness of the cornmeal. It is a simple yet satisfying treat that makes you feel at home.";
            public override rstring ishgardianTea => "As you take a sip of the Ishgardian tea, you immediately notice the rich aroma of the tea leaves, mixed with the sweet scent of maple sugar. The first thing you taste is the creaminess of the yak milk, which gives the tea a smooth and velvety texture. As the tea lingers on your tongue, you can taste the earthy flavor of the Coerthan tea leaves, balanced perfectly with the sweetness of the maple sugar. The overall taste is warm and comforting, like a hug in a cup.";
            public override rstring islandMiqabob => "As you take your first bite of the island miq'abob, you immediately notice the smoky aroma of the hamsa tenderloin mingling with the flavors of the paprika and mushroom. The meat is tender and juicy, with a slight char on the outside from the grill. The perilla oil adds a nutty and aromatic note to the kabob, enhancing the overall flavor profile. The peppers provide a slightly sweet and crisp texture that complements the meat perfectly.";
            public override rstring jackolantern => "As you take a bite of the jack-o-lantern, the soft pumpkin flesh gives way to the sticky, sweet syrup that coats your tongue. The flavor is a perfect balance of the earthy pumpkin and the sugary syrup, making your taste buds dance with delight. The beeswax that was used to preserve the pumpkin gives it a slightly waxy texture, but it only adds to the unique experience of eating this festive treat.";
            public override rstring jelliedCompote => "As you take a bite of the jellied compote, your taste buds are delighted with a burst of flavors from the mixed fruits. The compote's texture is firm yet yielding, thanks to the gelatin that holds the stewed fruit together. The beet sugar adds a touch of sweetness that enhances the fruit's natural flavors. The sensation of each square melting in your mouth is a delightful contrast to the chewy texture.";
            public override rstring jelliedHarcot => "As you take a bite of the jellied harcot, the sweetness of the fruit hits your tongue immediately, followed by the delicate tang of the lemonette. The gelatin gives the dessert a smooth, almost velvety texture that melts in your mouth. The harcot itself is soft but still has a slight crunch, adding a refreshing contrast to the dish. It's a perfect treat for a hot summer day, and you can't help but savor each bite.";
            public override rstring jerkedBeef => "As you take a bite of the jerked beef, you are met with a burst of savory flavor. The meat is tough, but the dryness of the jerking process makes it easy to chew. The blend of salt, pepper, sage, and nutmeg creates a delicious spice that lingers on your tongue. The buffalo sirloin is the perfect meat for this dish as it has a slightly gamey taste that complements the spices well.";
            public override rstring jerkedJhammel => "As you take a bite of the jerked jhammel, you immediately feel a spicy kick that lingers on your tongue. The meat is tough and chewy, but the seasonings add a depth of flavor that keeps you coming back for more. The salt and cumin bring a savory note, while the black pepper and oregano add a hint of earthiness. The dragon peppers, on the other hand, give a fiery heat that is not for the faint of heart.";
            public override rstring jhammelMoussaka => "As you take a bite of the jhammel moussaka, the tender and savory jhammel meat blends perfectly with the soft texture of the eggplant and zucchini, and the creamy mashed popotoes. The combination of flavors is well-balanced, with the spices enhancing the taste of the meat without overpowering it.";
            public override rstring jhingaBiryani => "As you take a bite of the jhinga biryani, your taste buds are immediately awakened by the explosion of flavors. The aroma of the seasoned bomba rice and perilla oil tantalizes your senses, while the succulent Thavnairian prawns add a seafood umami flavor to the dish. The rice is cooked to perfection, not too soft or hard, and the prawns are tender with a slightly crispy exterior. The sea salt and paprika elevate the overall taste of the biryani, giving it a well-rounded flavor that makes it hard to resist taking another bite.";
            public override rstring jhingaCurry => "As you take a bite of the jhinga curry, you feel the richness of the coconut milk and the aroma of the spices hitting your senses. The prawns are tender and flavorful, and the tomato adds a slight tang to the dish. The creamy texture of the curry contrasts nicely with the tender jhinga prawns. The nutmeg seeds add a warm, earthy flavor to the dish that brings everything together in perfect harmony.";
            public override rstring kaiserRoll => "As you bite into the kaiser roll, you notice the slight crunch from the sesame seeds on top, followed by the soft, chewy texture of the bread inside. The bread has a slightly sweet taste and a hint of nuttiness from the sesame seeds. The yak milk used in the recipe adds a unique depth of flavor to the bread. Despite being a simple bread, the kaiser roll has a satisfying and filling quality that makes it perfect for a variety of meals.";
            public override rstring kalamarakiaTiganita => "As you take a bite of kalamarakia tiganita, the crispy exterior of the fried calamari gives way to the tender and juicy squid inside. The dark rye breading provides a slight crunch and a deep, savory flavor that complements the delicate taste of the seafood. The dish is garnished with fresh parsley and a squeeze of lemonette, which adds a bright and tangy note to the overall taste.";
            public override rstring karniyarik => "As you take a bite of the karniyarik, the combination of the tender hamsa meat and flavorful spices immediately delights your taste buds. The eggplant has a satisfyingly soft texture that complements the meat, while the single slice of tomato adds a touch of acidity to the dish. The aroma of the paprika and nutmeg seeds wafts through your nose, enhancing the overall experience. As you savor each bite, you appreciate the depth of flavor that the perilla oil brings.";
            public override rstring kasha => "As you spoon the kasha into your mouth, you immediately notice the heartiness of the dish. The buckwheat kernels give the porridge a dense texture that fills you up quickly. The onion and dill lend a savory flavor that complements the nuttiness of the buckwheat. As you continue to eat, you find little pockets of richness from the egg mixed into the porridge. The warmth of the dish is comforting, making it a perfect meal for a cold day.";
            public override rstring kingCrabCake => "As you take a bite, you'll immediately notice the crispy texture of the wheat flour breading, giving way to the tender, juicy spiny crab meat inside. The flavor is savory with a hint of sweetness from the crab, perfectly complemented by the creamy herb sauce on top.";
            public override rstring kingSalmonMeuniere => "As you take a bite of the king salmon meuniere, you immediately notice the crispy texture of the breading that envelops the fish. The succulent flesh of the salmon is tender and falls apart easily in your mouth, complemented by the delicate notes of lemonette and black pepper that add a touch of zest to the dish. The fermented butter provides a rich, creamy finish that brings all the flavors together in a delightful harmony.";
            public override rstring kingUrchinLoaf => "As you take a bite of the king urchin loaf, the creamy texture and oceanic taste of the urchin filling spread over your tongue. The savory flavor of the creamtop mushroom and the richness of the coconut milk make a perfect complement to the delicate taste of the urchin. The loaf has a soft and spongy texture with just the right amount of firmness. The mixture of ingredients in the dish provides a unique and satisfying taste that makes you savor each bite.";
            public override rstring kingcake => "As you take a bite of the kingcake, you notice the soft and spongy texture of the brioche dough. The sweet aroma of the birch syrup and the yak milk fills your senses, and as you take another bite, you savor the delicate sweetness of the cake. The miniature moogle and tiny crown on top add a touch of whimsy to the presentation, making it a perfect dessert for any festive occasion.";
            public override rstring knightsBread => "As you break off a piece of Knight's Bread, you can't help but notice its tough, crusty exterior. The inside, however, is soft and chewy. The bread's rich aroma, infused with a hint of honey and basil, fills your nostrils. You take a bite, and the flavors burst in your mouth. The sweetness of honey perfectly balances the earthy taste of rye, while the basil adds a subtle, refreshing note. Despite its tough exterior, the bread is surprisingly easy to chew and goes down smoothly.";
            public override rstring konpeito => "As you pop one of the small, colorful konpeito candies into your mouth, you're met with a satisfying crunch. The hard, crystal-like exterior quickly dissolves into a smooth, sweet, and almost buttery texture on your tongue. The flavor is a delightful blend of sweetness from the beet sugar and a hint of floral notes from the honey. Looking at the tiny candies, each with their own unique shape and color, you can't help but appreciate the artistry that went into creating such a simple yet beautiful treat.";
            public override rstring kukuruRusk => "As you take a bite of the kukuru rusk, you are struck by its deep brown color and the enticing aroma of cocoa. The bread is crispy and light, with a satisfying crunch that gives way to a tender, almost flaky texture as you chew. The rich flavor of kukuru is at the forefront, and is complemented by the subtle sweetness of the butter and the slight saltiness of the salt crystals. Overall, the kukuru rusk is a delicious and satisfying treat that is perfect for a midday snack or a light dessert.";
            public override rstring laNosceanToast => "As you cut into the golden-brown, thick slice of La Noscean toast, the aroma of warm, buttery bread mingled with the sweet scent of maple syrup wafts to your nose. The exterior is crispy and slightly caramelized, while the soft, pillowy interior of the walnut bread absorbs the rich flavors of the buffalo milk and egg batter. With each bite, the smooth and creamy texture of the toast is perfectly balanced by the slightly crunchy walnuts scattered throughout. The sweetness of the maple syrup and the savory notes of the butter and olive oil create a harmonious blend of flavors that linger on your taste buds.";
            public override rstring laghman => "As you take a first look at the steaming bowl of laghman in front of you, you notice the vibrant colors of the carrots and laurel leaves that have been cooked into the broth. The chewy, thick noodles rest at the bottom of the bowl, with the minced dzo chunks peeking through. As you take your first bite, you notice the rich, briny flavor of the fish broth that has been cooked into the noodles, complementing the tender and savory minced dzo. The noodles have a satisfying, hearty texture, and each bite leaves you feeling comforted and full.";
            public override rstring landtrapSalad => "Upon taking a bite, you are greeted by the contrast of the crisp lettuce, juicy tomatoes, and slight bitter taste of the landtrap leaves. It's overall earthiness is delicately cut from the sharpness from the onions. With a drizzle of fragrant olive oil, it all ties together with a rich, luxurious mouthfeel. The salad is flavorful and delicious - If you can get over that the Landtrap leaves are still, indeed, twitching and wiggling inside of your mouth and on your plate.";
            public override rstring lavaToadLegs => "As you take a bite of the lava toad leg, you feel the crunchy outer layer give way to the tender, juicy meat inside. The subtle flavor of the toad meat is complemented by the earthy taste of snails and the spicy heat of dragon pepper. The batter is crisp and slightly salty, while the wheat and basil create a pleasant aroma. Overall, the dish is hearty and satisfying, perfect for adventurers looking for a filling meal, if the unusual ingredients don't scare you off at first.";
            public override rstring lemonCurdSachertorte => "With a quick glance, you can already tell that this lemon curd sachertorte is going to be something special. The shiny chocolate glaze glistens invitingly, and the layers of moist chocolate cake look dense and rich. As you take a bite, the first thing you notice is the tangy lemon curd filling, which cuts through the sweetness of the chocolate and provides a bright, refreshing flavor. The cake itself is perfectly moist and tender, with just the right amount of crumbly texture. It's a perfect balance of rich chocolate and zesty lemon, and it leaves you feeling satisfied and content.";
            public override rstring lemonMuffin => "With your first bite of the lemon muffin, you are struck by the bright citrus flavor that dances across your tongue. The muffin is moist and tender, with a delicate crumb that practically melts in your mouth. As you take another bite, you notice the subtle hint of cardamom that lingers on your taste buds. The muffin is not overly sweet, allowing the tartness of the lemon to shine through.";
            public override rstring lemonWaffle => "With its golden brown exterior, the lemon waffle looks crisp and inviting. As you take a bite, the crispiness gives way to a soft and airy interior that melts in your mouth. The slight tang of the lemonette pairs perfectly with the sweetness of the beet sugar, creating a delightful balance of flavors. The waffle's texture is both fluffy and slightly chewy, making each bite satisfyingly delicious. The lemon waffle is a perfect treat for breakfast or dessert.";
            public override rstring lemonade => "With each sip, you feel a burst of zesty and tangy flavors in your mouth, followed by a delicate sweetness. The lemonade's bright yellow hue seems to shine even brighter when in the sun, and its clarity hints at its purity. The texture is smooth and slightly syrupy, leaving a lingering sweetness on your lips. As you continue to drink, the crisp, clean taste of the spring water used to make the lemonade adds a refreshing finish to this drink that is perfect for a hot summer day.";
            public override rstring lentilCurry => "As you savor the first bite, the complex flavors of the lentil curry unfold on your palate. The nutty lentils and tender carrots are cooked to perfection, while the creamy coconut milk lends a subtle sweetness that perfectly balances the bold flavors of the spices. The fragrant aroma of nutmeg fills your senses as you scoop up another spoonful of this hearty and satisfying dish. The soft and fluffy bomba rice soaks up the flavors of the curry, enhancing each bite.";
            public override rstring lentilsAndChestnuts => "You take a spoonful of the lentil and chestnut stew and savor the rich and earthy flavors. The nuttiness of the chestnuts complements the creamy lentils, while the mint adds a refreshing touch. The stew has a thick and hearty texture, and the olive oil gives it a smooth and silky finish. As you take another bite, you notice a subtle hint of black pepper that adds a slight kick to the dish.";
            public override rstring livercheeseSandwich => "With each bite of the liver-cheese sandwich, you savor the rich and savory flavors of the ground liver, blended to perfection with creamy cream cheese. The bread of the kaiser roll is lightly toasted, providing a pleasant contrast to the soft and smooth texture of the liver-cheese filling.";
            public override rstring loaghtanCordonBleu => "Glistening with a crispy exterior, the golden-brown cordon bleu immediately catches your eye. As you cut into the meat, the aroma of smoked bacon wafts up, accompanied by the unmistakable scent of stone cheese. The texture is delightfully tender, with the flavors of the cheese and bacon blending harmoniously with the succulent loaghtan fillet.";
            public override rstring loquatJuice => "You hold the tall glass of loquat juice up to the light, admiring its glowing orange color. As you bring it to your lips, the aroma of fresh loquats mixed with a hint of lemon tickles your nose. With the first sip, you feel the velvety texture of the juice in your mouth, and you taste the sweet and tangy flavor of the ripe loquats, perfectly balanced with a touch of tartness from the lemon and a subtle sweetness from the honey. Each subsequent sip fills your mouth with the refreshing taste of the delicious fruit.";
            public override rstring marronGlace => "You gaze at the exquisite marron glacé, its glossy surface gleaming under the soft lighting. With anticipation, you take a delicate piece between your fingers, feeling its smooth and firm texture. As you place it on your tongue, the sweetness of the dark chestnut fills your mouth, mingling with the subtle notes of the birch syrup and a hint of richness from the sherry. The chestnut is tender and luscious, almost melting in your mouth, leaving behind a lingering warmth and a delightful sweetness that lingers on your palate.";
            public override rstring masalaChai => "As you lift the steaming cup of masala chai to your lips, you marvel at the rich, golden-brown hue of the fragrant brew. The aroma of amber cloves and cinnamon wafts up, enveloping your senses. Your first sip reveals a perfect balance of warmth and spice, as the flavors of the cloves, and cinnamon dance on your tongue. The creamy texture from the milk adds a soothing touch. Each sip offers a delightful combination of sweetness and aromatic complexity, leaving you craving for more.";
            public override rstring mashedPopotoes => "As you dig your fork into the fluffy mound of mashed popotoes, you can't help but admire its creamy, pale-yellow appearance. The texture is incredibly smooth and velvety, as each bite melts in your mouth. The combination of popotoes and cottage cheese creates a delightful creaminess, enhanced by the richness of butter and sweet cream. A subtle hint of garlic adds a savory note that complements the natural sweetness of the popotoes. With each bite, you savor the comforting flavors and the harmonious balance of tastes.";
            public override rstring matcha => "With your hands gently cradling the delicate matcha tea bowl, you admire the vibrant green hue of the liquid within. The texture is velvety smooth, almost like a thin silk robe gliding over your palate. As you bring the bowl to your lips, the aroma of freshly whisked matcha envelops your senses, transporting you to a serene tea garden. The taste is distinctly bitter yet captivating, with a hint of earthiness that lingers on your tongue. Each sip is a moment of tranquility, as you appreciate the intricate art of whisking the fine-ground tea leaves into the steaming hot water. Drinking the tea is a ritual that requires grace and attention, inviting you to savor the depth of flavor and find harmony in every sip.";
            public override rstring meatMiqabob => "Beneath the soft moonlight, you hold the skewered miq'abob, mesmerized by its rustic beauty. The juicy dodo meat and vibrant ruby tomatoes are meticulously arranged, their colors intermingling like a captivating work of art. As you take a bite, the tender meat yields effortlessly, revealing its succulent and smoky essence. The burst of flavors dances on your palate, with the slight tanginess of the tomatoes balancing the rich and savory meat. The texture is divine, a harmonious blend of melt-in-your-mouth tenderness and a satisfying juiciness. Each bite is a delightful symphony of flavors, a testament to the traditional Keeper of the Moon craftsmanship that brings this miq'abob to life.";
            public override rstring mejillonesalAjillo => "With anticipation, you gaze upon the mejillones al ajillo before you. The plump mussels, nestled in their shells, are adorned with a luscious coating of golden garlic sauce. The aroma of sizzling garlic and fragrant parsley wafts through the air, enticing your senses. As you reach for a mussel and bring it to your lips, the tender flesh yields effortlessly, revealing its delicate texture. The garlic-infused oil caresses your tongue, and the savory notes of the mussel intertwine with the aromatic garlic, while the freshness of parsley adds a delightful herbaceous touch. Each succulent bite is a testament to the ocean's bounty, a harmonious balance of flavors that leaves you longing for more.";
            public override rstring melonJuice => "Sitting in front of you is a glass of fresh melon juice made from sweet and succulent thundermelons. The juice is a light orange color with tiny specks of nutmeg seeds. As you bring the glass to your lips, you feel the coolness of the juice and notice its slightly thick texture. You take a sip and feel the sweet and refreshing taste burst in your mouth, followed by a subtle hint of nutmeg. The juice is not too sweet, with just the right balance of flavor and sweetness. It's a refreshing drink that will cool you down on a hot day.";
            public override rstring melonPie => "";
            public override rstring mintLassi => "";
            public override rstring misoDengaku => "";
            public override rstring misoSoupWithTofu => "";
            public override rstring mistSpinachQuiche => "";
            public override rstring mistSpinachSaute => "";
            public override rstring mizzenmastBiscuit => "";
            public override rstring moleLoaf => "";
            public override rstring morelSalad => "";
            public override rstring mors => "";
            public override rstring mugwortCarp => "";
            public override rstring mulledTea => "";
            public override rstring mushroomSaute => "";
            public override rstring mushroomSkewer => "";
            public override rstring mustardEggs => "";
            public override rstring muttonStew => "";
            public override rstring nomadMeatPie => "";
            public override rstring nutBake => "";
            public override rstring oden => "";
            public override rstring onigarayaki => "";
            public override rstring orangeJuice => "";
            public override rstring oreFruitcake => "";
            public override rstring orobonStew => "";
            public override rstring ovimCordonBleu => "";
            public override rstring ovimMeatballs => "";
            public override rstring oysterConfit => "";
            public override rstring oystersontheHalfShell => "";
            public override rstring paella => "";
            public override rstring panfriedMahimahi => "";
            public override rstring papanasi => "";
            public override rstring parsnipSalad => "";
            public override rstring pastaOrtolano => "";
            public override rstring pastryFish => "";
            public override rstring peaSoup => "";
            public override rstring peachJuice => "";
            public override rstring peachTart => "";
            public override rstring pearlChocolate => "";
            public override rstring peperoncino => "";
            public override rstring pepperedPopotoes => "";
            public override rstring pepperoniPizza => "";
            public override rstring persimmonLeafSushi => "";
            public override rstring persimmonPudding => "";
            public override rstring philosophersSandwich => "";
            public override rstring pickledHerring => "";
            public override rstring piennoloTomatoSalad => "";
            public override rstring pineappleJuice => "";
            public override rstring pineapplePonzecake => "";
            public override rstring pineappleSalad => "";
            public override rstring pixieApplePie => "";
            public override rstring pixieberryCheesecake => "";
            public override rstring pixieberryTea => "";
            public override rstring pizza => "";
            public override rstring popotoPancakes => "";
            public override rstring popotoSalad => "";
            public override rstring popotoSoba => "";
            public override rstring popotoesauGratin => "";
            public override rstring porkKakuni => "";
            public override rstring porkStew => "";
            public override rstring priestlyOmelette => "";
            public override rstring princessPudding => "";
            public override rstring pumpkinPotage => "";
            public override rstring pumpkinRatatouille => "";
            public override rstring purpleCarrotJuice => "";
            public override rstring rabbitPie => "";
            public override rstring raisins => "";
            public override rstring raptorStew => "";
            public override rstring rareRoastBeef => "";
            public override rstring ratatouille => "";
            public override rstring rawOyster => "";
            public override rstring risottoalNero => "";
            public override rstring roastCanard => "";
            public override rstring roastDodo => "";
            public override rstring roastOvim => "";
            public override rstring roastedNopales => "";
            public override rstring robeLettuceSalad => "";
            public override rstring rolanberryCheesecake => "";
            public override rstring rolanberryLassi => "";
            public override rstring rolanberryShavedIce => "";
            public override rstring rooibosTea => "";
            public override rstring roostBiscuit => "";
            public override rstring royalEggs => "";
            public override rstring sachertorte => "";
            public override rstring salmonMeuniere => "";
            public override rstring salmonMuffin => "";
            public override rstring saltCod => "";
            public override rstring saltCodPuffs => "";
            public override rstring saltedThavnairianCod => "";
            public override rstring sauerkraut => "";
            public override rstring sausageandSauerkraut => "";
            public override rstring sausageLinks => "";
            public override rstring sauteedCoeurl => "";
            public override rstring sauteedGreenLeeks => "";
            public override rstring sauteedPorcini => "";
            public override rstring scallopCurry => "";
            public override rstring scallopSalad => "";
            public override rstring scrambledEggs => "";
            public override rstring seafoodStew => "";
            public override rstring sermonworthyMeuniere => "";
            public override rstring sesameCookie => "";
            public override rstring shakshouka => "";
            public override rstring sharkFinSoup => "";
            public override rstring shepherdsPie => "";
            public override rstring shorlog => "";
            public override rstring sideritisCookie => "";
            public override rstring silkiePudding => "";
            public override rstring skyr => "";
            public override rstring smokedChicken => "";
            public override rstring smokedRaptor => "";
            public override rstring snowflakePeak => "";
            public override rstring snurbleberryTart => "";
            public override rstring sohmAlTart => "";
            public override rstring spaghettialNero => "";
            public override rstring spaghettiCarbonara => "";
            public override rstring spaghettiPescatore => "";
            public override rstring spicedCider => "";
            public override rstring spicyShakshouka => "";
            public override rstring spinachQuiche => "";
            public override rstring spinachSaute => "";
            public override rstring sprigganChocolate => "";
            public override rstring starlightLog => "";
            public override rstring steamedCatfish => "";
            public override rstring steamedGrouper => "";
            public override rstring steamedStaff => "";
            public override rstring steppeSalad => "";
            public override rstring steppeTea => "";
            public override rstring stewedRiverBream => "";
            public override rstring stoneSoup => "";
            public override rstring stuffedArtichoke => "";
            public override rstring stuffedCabbage => "";
            public override rstring stuffedCabbageRolls => "";
            public override rstring stuffedChysahl => "";
            public override rstring stuffedHighlandCabbage => "";
            public override rstring sunsetCarrotNibbles => "";
            public override rstring sweetandSourFrogsLegs => "";
            public override rstring sweetGnomefish => "";
            public override rstring sweetRiceCake => "";
            public override rstring sykonBavarois => "";
            public override rstring sykonCompote => "";
            public override rstring sykonCookie => "";
            public override rstring sykonSalad => "";
            public override rstring tailormadeEelPie => "";
            public override rstring takoyaki => "";
            public override rstring tempuraPlatter => "";
            public override rstring thavnairianChai => "";
            public override rstring noodlesOfElpis => "";
            public override rstring tofuPancakes => "";
            public override rstring tomatoPie => "";
            public override rstring trappersQuiche => "";
            public override rstring treeToadLegs => "";
            public override rstring tripleCreamCoffee => "";
            public override rstring tsaitouVounou => "";
            public override rstring tunaMiqabob => "";
            public override rstring twilightPopotoSalad => "";
            public override rstring ukha => "";
            public override rstring urchinLoaf => "";
            public override rstring urchinPasta => "";
            public override rstring walnutBread => "";
            public override rstring warriorsStew => "";
            public override rstring wildwoodScrambledEggs => "";
            public override rstring winedarkSoup => "";
            public override rstring yakowMoussaka => "";
            public override rstring yearsOldPumpkinCookie => "";
            public override rstring zefir => "";
            public override rstring zoni => "";
            public override rstring zurek => "";
        }
    }
}
