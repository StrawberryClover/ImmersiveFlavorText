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
    }
}
