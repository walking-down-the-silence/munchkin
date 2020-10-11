using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Cards.Rules;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class HornyHelmet : PermanentItemCard
    {
        public HornyHelmet() : base("Horny Helmet", 1, 0, EItemSize.Small, EWearingType.Headgear, 600)
        {
            AddEffect(Effect
                .New(new PlayerStrengthBonusEffect(2))
                .With(() => Rule
                    .New(new HasElfRaceRule())));
        }
    }
}