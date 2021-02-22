using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Rules;

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