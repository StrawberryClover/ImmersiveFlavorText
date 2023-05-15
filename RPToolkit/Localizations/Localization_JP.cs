using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Localizations
{
    internal class Localization_JP : LocalizationBase
    {
        public override TemperatureStages temperatureStages => new TemperatureStages(
            heatwave: new TemperatureDescription(
                increaseDesc: new string[]{
                    "いつまでこの暑さに耐えられるかわかりません。我慢できない。クールダウンに役立つものを見つけるために何をしなければなりませんか?",
                    "この灼熱の暑さにこれ以上耐えることはほとんどできません。耐えられません。休息を見つけて冷静になるためなら何でもします",
                    "この暑さはうだるようです。"
                },
                decreaseDesc: new string[]{
                    "Nobody should see this message."
                }),
            veryHot: new TemperatureDescription(
                increaseDesc: new string[]{
                    "あなたは今、少し暑く感じ始めています。実際、とても暑いです。",
                    "あなたはかなり過熱していると感じ始めます。"
                },
                decreaseDesc: new string[]{
                    "暑さがより耐えられるようになり始めます。",
                    "熱の強さが弱まり始め、より耐えられるようになります。"
                }),
            hot: new TemperatureDescription(
                increaseDesc: new string[]{
                    "暑くなり始めたので、涼むのに役立つものがあるといいでしょう。",
                    "上昇する暑さに不快感を覚え始め、休息とクールダウンに役立つ何かを切望します。"
                },
                decreaseDesc: new string[]{
                    "以前ほど暑くはありませんが、もう少し涼しくなるといいですね。",
                    "温度が以前の激しさからわずかに下がると、安堵を感じます。"
                }),
            roomTemp: new TemperatureDescription(
                increaseDesc: new string[]{
                    "気温はちょうどいいみたいです。",
                    "今の気温がちょうどいいようで、心地よい感覚を醸し出しています。",
                    "温度が上がるにつれて、理想的な暖かさのレベルに達します。"
                },
                decreaseDesc: new string[]{
                    "今がちょうどいい気温のようです。",
                    "今の気温がちょうどいいようで、心地よい感覚を醸し出しています。"
                }),
            mild: new TemperatureDescription(
                increaseDesc: new string[]{
                    "少し暖かく感じ始めます。",
                    "温度は適度なレベルまで上昇します。"
                },
                decreaseDesc: new string[]{
                    "少し気温が下がってきたようで、少しだけ暖かく感じ始めました。",
                    "温度は緩やかに下がります。"
                }),
            lukewarm: new TemperatureDescription(
                increaseDesc: new string[]{
                    "物事は少し暖かくなっているように見えますが、今はあなたには生ぬるく感じています.",
                    "ウォームアップを開始します。"
                },
                decreaseDesc: new string[]{
                    "気温が下がるにつれてぬるく感じ始めます。"
                }),
            chilled: new TemperatureDescription(
                increaseDesc: new string[]{
                    "寒さは以前ほどひどくはなく、ほんの少しだけ寒さを感じ始めます。",
                    "以前に比べて寒さにも耐えられるようになり、体が少し冷えるようになったようです。",
                    "気温が上がり、肌寒くなってきました。"
                },
                decreaseDesc: new string[]{
                    "You start to feel chilly, things are starting to cool off quite a bit now.",
                    "You begin to feel a chill in the air, as things are noticeably cooling down."
                }),
            cold: new TemperatureDescription(
                increaseDesc: new string[]{
                    "You no longer feel so cold. You haven't stopped shivering, but it almost feels bearable now.",
                    "You start to warm up slightly, it's becoming more tolerable than before.",
                    "You are starting to warm up, though you still feel a chill, but not as much as before."
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
            public override rstring grilledMeat => "";
            public override rstring grilledFish => "";




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
            public override rstring beefStroganoff => "You'll feel the warmth of the dish as you take a bite of the tender beef, and enjoy the creamy texture of the sauce as it coats your tongue. The stroganoff will appear rich and hearty, with chunks of potatoes and beef mixed in with the sauce. The taste will be savory, with a hint of spiciness from the spices and the slight bitterness of lettuce, providing a well-rounded flavor.";
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
            public override rstring chilledPopotoSoup => "As you gaze upon the chilled potato soup, you can see its creamy, pale yellow color, speckled with bits of green parsley. As you take a spoonful, you feel the cool temperature and the smooth, velvety texture of the soup on your tongue. The taste is refreshing and savory, with a slight sweetness from the leeks and cream and a hint of herbaceousness from the parsley.";
            public override rstring chirashizushi => "With a colorful mix of ingredients arranged beautifully over a bed of sticky rice, the chirashi-zushi looks like a work of art. As you take a bite, the texture is tender and moist, with a slight crunch from the lotus root. The temperature is cool, perfect for a refreshing bite. The taste is a perfect balance of sweetness from the rice vinegar, the umami flavor of the shiitake mushrooms, the savory taste of the tiger prawn, and the subtle richness of the eggs.";
            public override rstring clamChowder => "";
            public override rstring cockatriceMeatballs => "";
            public override rstring coconutCodChowder => "";
            public override rstring coffeeBiscuit => "";
            public override rstring cornbread => "";
            public override rstring crabCakes => "";
            public override rstring crabCroquette => "";
            public override rstring creamySalmonPasta => "";
            public override rstring cremeBrulee => "";
            public override rstring crimsonCider => "";
            public override rstring crumpet => "";
            public override rstring daggerSoup => "";
            public override rstring darkPretzel => "";
            public override rstring deepfriedOkeanis => "";
            public override rstring deviledEggs => "";
            public override rstring dhalmelFricassee => "";
            public override rstring dhalmelGratin => "";
            public override rstring domanTea => "";
            public override rstring driedPlums => "";
            public override rstring dzemaelGratin => "";
            public override rstring eelPie => "";
            public override rstring eggFooYoung => "";
            public override rstring elpisDeipnon => "";
            public override rstring emaDatshi => "";
            public override rstring emeraldSoup => "";
            public override rstring espressoConPanna => "";
            public override rstring exquisiteBeefStew => "";
            public override rstring farmersBreakfast => "";
            public override rstring figBavarois => "";
            public override rstring fingerSandwich => "";
            public override rstring fishSoup => "";
            public override rstring fishStew => "";
            public override rstring flatbread => "";
            public override rstring flaugnarde => "";
            public override rstring flintCaviar => "";
            public override rstring forestMiqabob => "";
            public override rstring friedEgg => "";
            public override rstring frozenSpirits => "";
            public override rstring frumenty => "";
            public override rstring futomakiRoll => "";
            public override rstring gameni => "";
            public override rstring garleanPizza => "";
            public override rstring giantHaddockDip => "";
            public override rstring giantPopotoPancakes => "";
            public override rstring gingerCookie => "";
            public override rstring gingerSalad => "";
            public override rstring gloryBeSoup => "";
            public override rstring goldenPineappleJuice => "";
            public override rstring grapeJuice => "";
            public override rstring grilledCarp => "";
            public override rstring grilledDodo => "";
            public override rstring grilledTurban => "";
            public override rstring gyros => "";
            public override rstring haddockDip => "";
            public override rstring hamsaCurry => "";
            public override rstring happinessJuice => "";
            public override rstring heavenlyEggnog => "";
            public override rstring heavenseggSoup => "";
            public override rstring herringPie => "";
            public override rstring honeyBun => "";
            public override rstring honeyCroissant => "";
            public override rstring honeyMuffin => "";
            public override rstring hotChocolate => "";
            public override rstring hourglassBiscuit => "";
            public override rstring imamBayildi => "";
            public override rstring ishgardianMuffin => "";
            public override rstring ishgardianTea => "";
            public override rstring islandMiqabob => "";
            public override rstring jackolantern => "";
            public override rstring jelliedCompote => "";
            public override rstring jelliedHarcot => "";
            public override rstring jerkedBeef => "";
            public override rstring jerkedJhammel => "";
            public override rstring jhammelMoussaka => "";
            public override rstring jhingaBiryani => "";
            public override rstring jhingaCurry => "";
            public override rstring kaiserRoll => "";
            public override rstring kalamarakiaTiganita => "";
            public override rstring karniyarik => "";
            public override rstring kasha => "";
            public override rstring kingCrabCake => "";
            public override rstring kingSalmonMeuniere => "";
            public override rstring kingUrchinLoaf => "";
            public override rstring kingcake => "";
            public override rstring knightsBread => "";
            public override rstring konpeito => "";
            public override rstring kukuruRusk => "";
            public override rstring laNosceanToast => "";
            public override rstring laghman => "";
            public override rstring landtrapSalad => "";
            public override rstring lavaToadLegs => "";
            public override rstring lemonCurdSachertorte => "";
            public override rstring lemonMuffin => "";
            public override rstring lemonWaffle => "";
            public override rstring lemonade => "";
            public override rstring lentilCurry => "";
            public override rstring lentilsAndChestnuts => "";
            public override rstring livercheeseSandwich => "";
            public override rstring loaghtanCordonBleu => "";
            public override rstring loquatJuice => "";
            public override rstring marronGlace => "";
            public override rstring masalaChai => "";
            public override rstring mashedPopotoes => "";
            public override rstring matcha => "";
            public override rstring meatMiqabob => "";
            public override rstring mejillonesalAjillo => "";
            public override rstring melonJuice => "";
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
