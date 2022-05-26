using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseOneBigItem : CurseCard
    {
        public LoseOneBigItem() : 
            base(MunchkinDeluxeCards.Doors.LoseOneBigItem, "Lose One Big Item")
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