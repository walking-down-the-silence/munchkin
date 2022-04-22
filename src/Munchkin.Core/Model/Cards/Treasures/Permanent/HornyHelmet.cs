using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Rules;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
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