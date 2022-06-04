using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Restrictions;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class MaulRat : MonsterCard
    {
        public MaulRat() : 
            base(MunchkinDeluxeCards.Doors.MaulRat, "Maul Rat", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(3))
                .With(() => Rule
                    .New(new UsableByClericOnlyRestriction())));
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.LevelDown();
            return table;
        }
    }
}