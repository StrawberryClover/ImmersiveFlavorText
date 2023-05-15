using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Localizations
{
    public abstract class LocalizationBase
    {
        public abstract TemperatureStages temperatureStages { get; }
        public abstract rstring rainStarts { get; }
        public abstract rstring rainEnds { get; }
        public abstract BaseConsumableDescriptions consumables { get; }

        public abstract class BaseConsumableDescriptions
        {
            public abstract rstring steak { get; }
            public abstract rstring omelette { get; }
            public abstract rstring chocolate { get; }
            public abstract rstring whiteChocolate { get; }
            public abstract rstring grilledMeat { get; }
            public abstract rstring grilledFish { get; }



            public abstract rstring acornCookie { get; }
            public abstract rstring alligatorSalad { get; }
            public abstract rstring almondCreamCroissant { get; }
            public abstract rstring amraLassi { get; }
            public abstract rstring amraSalad { get; }
            public abstract rstring anglerStew { get; }
            public abstract rstring antelopeStew { get; }
            public abstract rstring appleJuice { get; }
            public abstract rstring appleStrudel { get; }
            public abstract rstring appleTart { get; }
            public abstract rstring archonBurger { get; }
            public abstract rstring archonLoaf { get; }
            public abstract rstring arrosNegre { get; }
            public abstract rstring baconBread { get; }
            public abstract rstring baconBroth { get; }
            public abstract rstring baguette { get; }
            public abstract rstring bakedMegapiranha { get; }
            public abstract rstring bakedOnionSoup { get; }
            public abstract rstring bakedPipiraPira { get; }
            public abstract rstring bakedSole { get; }
            public abstract rstring baklava { get; }
            public abstract rstring banhXeo { get; }
            public abstract rstring batteredFish { get; }
            public abstract rstring beefStew { get; }
            public abstract rstring beefStroganoff { get; }
            public abstract rstring beetSoup { get; }
            public abstract rstring crownedPie { get; }
            public abstract rstring candyDrop { get; }
            public abstract rstring blackTruffleRisotto { get; }
            public abstract rstring bloodCurrantTart { get; }
            public abstract rstring tomatoJuice { get; }
            public abstract rstring tomatoSalad { get; }
            public abstract rstring boiledAmberjackHead { get; }
            public abstract rstring boiledBream { get; }
            public abstract rstring boiledCrayfish { get; }
            public abstract rstring boiledEgg { get; }
            public abstract rstring borscht { get; }
            public abstract rstring boscaiola { get; }
            public abstract rstring bouillabaisse { get; }
            public abstract rstring braisedPipira { get; }
            public abstract rstring broadBeanCurry { get; }
            public abstract rstring broadBeanSalad { get; }
            public abstract rstring broadBeanSoup { get; }
            public abstract rstring buckwheatTea { get; }
            public abstract rstring butteryMogbiscuit { get; }
            public abstract rstring buttonMushroomSaute { get; }
            public abstract rstring buttonsInABlanket { get; }
            public abstract rstring calamariRipieni { get; }
            public abstract rstring caramels { get; }
            public abstract rstring carrotNibbles { get; }
            public abstract rstring carrotPudding { get; }
            public abstract rstring cawlCennin { get; }
            public abstract rstring chamomileTea { get; }
            public abstract rstring chanterelleSaute { get; }
            public abstract rstring charredCharr { get; }
            public abstract rstring chawanmushi { get; }
            public abstract rstring cheeseRisotto { get; }
            public abstract rstring cheeseSouffle { get; }
            public abstract rstring chickenAndMushrooms { get; }
            public abstract rstring chickenFettuccine { get; }
            public abstract rstring chiliCrab { get; }
            public abstract rstring chilledPopotoSoup { get; }
            public abstract rstring chirashizushi { get; }
            public abstract rstring clamChowder { get; }
            public abstract rstring cockatriceMeatballs { get; }
            public abstract rstring coconutCodChowder { get; }
            public abstract rstring coffeeBiscuit { get; }
            public abstract rstring cornbread { get; }
            public abstract rstring crabCakes { get; }
            public abstract rstring crabCroquette { get; }
            public abstract rstring creamySalmonPasta { get; }
            public abstract rstring cremeBrulee { get; }
            public abstract rstring crimsonCider { get; }
            public abstract rstring crumpet { get; }
            public abstract rstring daggerSoup { get; }
            public abstract rstring darkPretzel { get; }
            public abstract rstring deepfriedOkeanis { get; }
            public abstract rstring deviledEggs { get; }
            public abstract rstring dhalmelFricassee { get; }
            public abstract rstring dhalmelGratin { get; }
            public abstract rstring domanTea { get; }
            public abstract rstring driedPlums { get; }
            public abstract rstring dzemaelGratin { get; }
            public abstract rstring eelPie { get; }
            public abstract rstring eggFooYoung { get; }
            public abstract rstring elpisDeipnon { get; }
            public abstract rstring emaDatshi { get; }
            public abstract rstring emeraldSoup { get; }
            public abstract rstring espressoConPanna { get; }
            public abstract rstring exquisiteBeefStew { get; }
            public abstract rstring farmersBreakfast { get; }
            public abstract rstring figBavarois { get; }
            public abstract rstring fingerSandwich { get; }
            public abstract rstring fishSoup { get; }
            public abstract rstring fishStew { get; }
            public abstract rstring flatbread { get; }
            public abstract rstring flaugnarde { get; }
            public abstract rstring flintCaviar { get; }
            public abstract rstring forestMiqabob { get; }
            public abstract rstring friedEgg { get; }
            public abstract rstring frozenSpirits { get; }
            public abstract rstring frumenty { get; }
            public abstract rstring futomakiRoll { get; }
            public abstract rstring gameni { get; }
            public abstract rstring garleanPizza { get; }
            public abstract rstring giantHaddockDip { get; }
            public abstract rstring giantPopotoPancakes { get; }
            public abstract rstring gingerCookie { get; }
            public abstract rstring gingerSalad { get; }
            public abstract rstring gloryBeSoup { get; }
            public abstract rstring goldenPineappleJuice { get; }
            public abstract rstring grapeJuice { get; }
            public abstract rstring grilledCarp { get; }
            public abstract rstring grilledDodo { get; }
            public abstract rstring grilledTurban { get; }
            public abstract rstring gyros { get; }
            public abstract rstring haddockDip { get; }
            public abstract rstring hamsaCurry { get; }
            public abstract rstring happinessJuice { get; }
            public abstract rstring heavenlyEggnog { get; }
            public abstract rstring heavenseggSoup { get; }
            public abstract rstring herringPie { get; }
            public abstract rstring honeyBun { get; }
            public abstract rstring honeyCroissant { get; }
            public abstract rstring honeyMuffin { get; }
            public abstract rstring hotChocolate { get; }
            public abstract rstring hourglassBiscuit { get; }
            public abstract rstring imamBayildi { get; }
            public abstract rstring ishgardianMuffin { get; }
            public abstract rstring ishgardianTea { get; }
            public abstract rstring islandMiqabob { get; }
            public abstract rstring jackolantern { get; }
            public abstract rstring jelliedCompote { get; }
            public abstract rstring jelliedHarcot { get; }
            public abstract rstring jerkedBeef { get; }
            public abstract rstring jerkedJhammel { get; }
            public abstract rstring jhammelMoussaka { get; }
            public abstract rstring jhingaBiryani { get; }
            public abstract rstring jhingaCurry { get; }
            public abstract rstring kaiserRoll { get; }
            public abstract rstring kalamarakiaTiganita { get; }
            public abstract rstring karniyarik { get; }
            public abstract rstring kasha { get; }
            public abstract rstring kingCrabCake { get; }
            public abstract rstring kingSalmonMeuniere { get; }
            public abstract rstring kingUrchinLoaf { get; }
            public abstract rstring kingcake { get; }
            public abstract rstring knightsBread { get; }
            public abstract rstring konpeito { get; }
            public abstract rstring kukuruRusk { get; }
            public abstract rstring laNosceanToast { get; }
            public abstract rstring laghman { get; }
            public abstract rstring landtrapSalad { get; }
            public abstract rstring lavaToadLegs { get; }
            public abstract rstring lemonCurdSachertorte { get; }
            public abstract rstring lemonMuffin { get; }
            public abstract rstring lemonWaffle { get; }
            public abstract rstring lemonade { get; }
            public abstract rstring lentilCurry { get; }
            public abstract rstring lentilsAndChestnuts { get; }
            public abstract rstring livercheeseSandwich { get; }
            public abstract rstring loaghtanCordonBleu { get; }
            public abstract rstring loquatJuice { get; }
            public abstract rstring marronGlace { get; }
            public abstract rstring masalaChai { get; }
            public abstract rstring mashedPopotoes { get; }
            public abstract rstring matcha { get; }
            public abstract rstring meatMiqabob { get; }
            public abstract rstring mejillonesalAjillo { get; }
            public abstract rstring melonJuice { get; }
            public abstract rstring melonPie { get; }
            public abstract rstring mintLassi { get; }
            public abstract rstring misoDengaku { get; }
            public abstract rstring misoSoupWithTofu { get; }
            public abstract rstring mistSpinachQuiche { get; }
            public abstract rstring mistSpinachSaute { get; }
            public abstract rstring mizzenmastBiscuit { get; }
            public abstract rstring moleLoaf { get; }
            public abstract rstring morelSalad { get; }
            public abstract rstring mors { get; }
            public abstract rstring mugwortCarp { get; }
            public abstract rstring mulledTea { get; }
            public abstract rstring mushroomSaute { get; }
            public abstract rstring mushroomSkewer { get; }
            public abstract rstring mustardEggs { get; }
            public abstract rstring muttonStew { get; }
            public abstract rstring nomadMeatPie { get; }
            public abstract rstring nutBake { get; }
            public abstract rstring oden { get; }
            public abstract rstring onigarayaki { get; }
            public abstract rstring orangeJuice { get; }
            public abstract rstring oreFruitcake { get; }
            public abstract rstring orobonStew { get; }
            public abstract rstring ovimCordonBleu { get; }
            public abstract rstring ovimMeatballs { get; }
            public abstract rstring oysterConfit { get; }
            public abstract rstring oystersontheHalfShell { get; }
            public abstract rstring paella { get; }
            public abstract rstring panfriedMahimahi { get; }
            public abstract rstring papanasi { get; }
            public abstract rstring parsnipSalad { get; }
            public abstract rstring pastaOrtolano { get; }
            public abstract rstring pastryFish { get; }
            public abstract rstring peaSoup { get; }
            public abstract rstring peachJuice { get; }
            public abstract rstring peachTart { get; }
            public abstract rstring pearlChocolate { get; }
            public abstract rstring peperoncino { get; }
            public abstract rstring pepperedPopotoes { get; }
            public abstract rstring pepperoniPizza { get; }
            public abstract rstring persimmonLeafSushi { get; }
            public abstract rstring persimmonPudding { get; }
            public abstract rstring philosophersSandwich { get; }
            public abstract rstring pickledHerring { get; }
            public abstract rstring piennoloTomatoSalad { get; }
            public abstract rstring pineappleJuice { get; }
            public abstract rstring pineapplePonzecake { get; }
            public abstract rstring pineappleSalad { get; }
            public abstract rstring pixieApplePie { get; }
            public abstract rstring pixieberryCheesecake { get; }
            public abstract rstring pixieberryTea { get; }
            public abstract rstring pizza { get; }
            public abstract rstring popotoPancakes { get; }
            public abstract rstring popotoSalad { get; }
            public abstract rstring popotoSoba { get; }
            public abstract rstring popotoesauGratin { get; }
            public abstract rstring porkKakuni { get; }
            public abstract rstring porkStew { get; }
            public abstract rstring priestlyOmelette { get; }
            public abstract rstring princessPudding { get; }
            public abstract rstring pumpkinPotage { get; }
            public abstract rstring pumpkinRatatouille { get; }
            public abstract rstring purpleCarrotJuice { get; }
            public abstract rstring rabbitPie { get; }
            public abstract rstring raisins { get; }
            public abstract rstring raptorStew { get; }
            public abstract rstring rareRoastBeef { get; }
            public abstract rstring ratatouille { get; }
            public abstract rstring rawOyster { get; }
            public abstract rstring risottoalNero { get; }
            public abstract rstring roastCanard { get; }
            public abstract rstring roastDodo { get; }
            public abstract rstring roastOvim { get; }
            public abstract rstring roastedNopales { get; }
            public abstract rstring robeLettuceSalad { get; }
            public abstract rstring rolanberryCheesecake { get; }
            public abstract rstring rolanberryLassi { get; }
            public abstract rstring rolanberryShavedIce { get; }
            public abstract rstring rooibosTea { get; }
            public abstract rstring roostBiscuit { get; }
            public abstract rstring royalEggs { get; }
            public abstract rstring sachertorte { get; }
            public abstract rstring salmonMeuniere { get; }
            public abstract rstring salmonMuffin { get; }
            public abstract rstring saltCod { get; }
            public abstract rstring saltCodPuffs { get; }
            public abstract rstring saltedThavnairianCod { get; }
            public abstract rstring sauerkraut { get; }
            public abstract rstring sausageandSauerkraut { get; }
            public abstract rstring sausageLinks { get; }
            public abstract rstring sauteedCoeurl { get; }
            public abstract rstring sauteedGreenLeeks { get; }
            public abstract rstring sauteedPorcini { get; }
            public abstract rstring scallopCurry { get; }
            public abstract rstring scallopSalad { get; }
            public abstract rstring scrambledEggs { get; }
            public abstract rstring seafoodStew { get; }
            public abstract rstring sermonworthyMeuniere { get; }
            public abstract rstring sesameCookie { get; }
            public abstract rstring shakshouka { get; }
            public abstract rstring sharkFinSoup { get; }
            public abstract rstring shepherdsPie { get; }
            public abstract rstring shorlog { get; }
            public abstract rstring sideritisCookie { get; }
            public abstract rstring silkiePudding { get; }
            public abstract rstring skyr { get; }
            public abstract rstring smokedChicken { get; }
            public abstract rstring smokedRaptor { get; }
            public abstract rstring snowflakePeak { get; }
            public abstract rstring snurbleberryTart { get; }
            public abstract rstring sohmAlTart { get; }
            public abstract rstring spaghettialNero { get; }
            public abstract rstring spaghettiCarbonara { get; }
            public abstract rstring spaghettiPescatore { get; }
            public abstract rstring spicedCider { get; }
            public abstract rstring spicyShakshouka { get; }
            public abstract rstring spinachQuiche { get; }
            public abstract rstring spinachSaute { get; }
            public abstract rstring sprigganChocolate { get; }
            public abstract rstring starlightLog { get; }
            public abstract rstring steamedCatfish { get; }
            public abstract rstring steamedGrouper { get; }
            public abstract rstring steamedStaff { get; }
            public abstract rstring steppeSalad { get; }
            public abstract rstring steppeTea { get; }
            public abstract rstring stewedRiverBream { get; }
            public abstract rstring stoneSoup { get; }
            public abstract rstring stuffedArtichoke { get; }
            public abstract rstring stuffedCabbage { get; }
            public abstract rstring stuffedCabbageRolls { get; }
            public abstract rstring stuffedChysahl { get; }
            public abstract rstring stuffedHighlandCabbage { get; }
            public abstract rstring sunsetCarrotNibbles { get; }
            public abstract rstring sweetandSourFrogsLegs { get; }
            public abstract rstring sweetGnomefish { get; }
            public abstract rstring sweetRiceCake { get; }
            public abstract rstring sykonBavarois { get; }
            public abstract rstring sykonCompote { get; }
            public abstract rstring sykonCookie { get; }
            public abstract rstring sykonSalad { get; }
            public abstract rstring tailormadeEelPie { get; }
            public abstract rstring takoyaki { get; }
            public abstract rstring tempuraPlatter { get; }
            public abstract rstring thavnairianChai { get; }
            public abstract rstring noodlesOfElpis { get; }
            public abstract rstring tofuPancakes { get; }
            public abstract rstring tomatoPie { get; }
            public abstract rstring trappersQuiche { get; }
            public abstract rstring treeToadLegs { get; }
            public abstract rstring tripleCreamCoffee { get; }
            public abstract rstring tsaitouVounou { get; }
            public abstract rstring tunaMiqabob { get; }
            public abstract rstring twilightPopotoSalad { get; }
            public abstract rstring ukha { get; }
            public abstract rstring urchinLoaf { get; }
            public abstract rstring urchinPasta { get; }
            public abstract rstring walnutBread { get; }
            public abstract rstring warriorsStew { get; }
            public abstract rstring wildwoodScrambledEggs { get; }
            public abstract rstring winedarkSoup { get; }
            public abstract rstring yakowMoussaka { get; }
            public abstract rstring yearsOldPumpkinCookie { get; }
            public abstract rstring zefir { get; }
            public abstract rstring zoni { get; }
            public abstract rstring zurek { get; }
        }
    }

    public struct TemperatureStages
    {
        public TemperatureDescription heatwave, veryHot, hot, roomTemp, mild, lukewarm, chilled, cold, veryCold, frigid;

        public TemperatureStages (
            TemperatureDescription heatwave,
            TemperatureDescription veryHot,
            TemperatureDescription hot,
            TemperatureDescription roomTemp,
            TemperatureDescription mild,
            TemperatureDescription lukewarm,
            TemperatureDescription chilled,
            TemperatureDescription cold,
            TemperatureDescription veryCold,
            TemperatureDescription frigid)
        {
            this.heatwave = heatwave;
            this.veryHot = veryHot;
            this.hot = hot;
            this.roomTemp = roomTemp;
            this.mild = mild;
            this.lukewarm = lukewarm;
            this.chilled = chilled;
            this.cold = cold;
            this.veryCold = veryCold;
            this.frigid = frigid;
        }
    }
    public struct TemperatureDescription
    {
        public rstring increaseDesc;
        public rstring decreaseDesc;

        public TemperatureDescription(rstring increaseDesc, rstring decreaseDesc)
        {
            this.increaseDesc = increaseDesc;
            this.decreaseDesc = decreaseDesc;
        }
    }

    ///<summary>Represents an array of strings, returning a random string as a value.</summary>
    public class rstring
    {
        public string? value { get { return GetRandomValue(values); } }
        public string[] values;

        public rstring(string[] values)
        {
            this.values = values;
        }

        private string? GetRandomValue(string[] values)
        {
            if (values.Length > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, values.Length);
                return values[index];
            }
            else return null;
        }

        public static implicit operator rstring(string[] values)
        {
            if (values == null) return null;
            else return new rstring(values);
        }

        public static implicit operator rstring(string value)
        {
            if (value == null) return null;
            else return new rstring(new string[] { value });
        }

        public static implicit operator string(rstring rs)
        {
            return rs.value;
        }
        public override string ToString() => value;
    }
}
