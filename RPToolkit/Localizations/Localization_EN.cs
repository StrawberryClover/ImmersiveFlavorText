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
                    "You begin to feel hot, it would be nice to have something to help you cool off.",
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
    }
}
