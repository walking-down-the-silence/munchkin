using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class IncomeTax : CurseCard
    {
        public IncomeTax() : 
            base(MunchkinDeluxeCards.Doors.IncomeTax, "Income Tax")
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