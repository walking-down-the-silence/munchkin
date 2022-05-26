using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Pitbull : MonsterCard
    {
        public Pitbull() :
            base(MunchkinDeluxeCards.Doors.PitBull, "Pit Bull", 2, 1, 1, 0, false)
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            throw new NotImplementedException();
        }
    }
}