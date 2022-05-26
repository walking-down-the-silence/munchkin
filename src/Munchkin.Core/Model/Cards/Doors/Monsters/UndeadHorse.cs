using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class UndeadHorse : MonsterCard
    {
        public UndeadHorse() :
            base(MunchkinDeluxeCards.Doors.UndeadHorse, "Undead Horse", 4, 1, 2, 0, true)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(5))
                .With(() => Rule
                    .New(new UsableByDwarfOnlyRestriction())));
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.LevelDown(2);

            return table;
        }
    }
}