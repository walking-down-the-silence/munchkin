using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using Munchkin.Core.Model.Rules;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class ShieldOfUbiquity : WearingCard
    {
        public ShieldOfUbiquity() :
            base(MunchkinDeluxeCards.Treasures.ShieldOfUbiquity, "Shield Of Ubiquity", 0, 0, EItemSize.Big, EWearingType.OneHanded, 600)
        {
            AddRestriction(new UsableByWarriorOnlyRestriction());
            AddEffect(Effect
                .New(new PlayerStrengthBonusEffect(4))
                .With(() => Rule
                    .New(new CanCarryBigItemRule())
                    .And(new HasOneFreeHandRule())
                    .And(new UsableByWarriorOnlyRestriction())));
        }

        public override Task Play(Table context)
        {
            // apply dynamic effects if conditions are met
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return Task.CompletedTask;
        }
    }
}