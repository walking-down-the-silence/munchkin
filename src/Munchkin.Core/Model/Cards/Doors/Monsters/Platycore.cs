using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Platycore : MonsterCard
    {
        public Platycore() :
            base(MunchkinDeluxeCards.Doors.Platycore, "Platycore", 6, 1, 2, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(6))
                .With(() => Rule
                    .New(new UsableByWizardOnlyRestriction())));
        }

        public override Task Play(Table table)
        {
            // apply dynamic effects if conditions are met
            Effects.Where(effect => effect.Satisfies(table)).ForEach(effect => effect.Apply(table));

            return base.Play(table);
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            throw new NotImplementedException();
        }
    }
}