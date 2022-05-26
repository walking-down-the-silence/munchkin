using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class SnailsOnSpeed : MonsterCard
    {
        public SnailsOnSpeed() :
            base(MunchkinDeluxeCards.Doors.SnailsOnSpeed, "Snails On Speed", 4, 1, 2, -2, false)
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