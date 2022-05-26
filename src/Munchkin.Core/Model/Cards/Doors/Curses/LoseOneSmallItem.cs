using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseOneSmallItem : CurseCard
    {
        public LoseOneSmallItem() : 
            base(MunchkinDeluxeCards.Doors.LoseOneSmallItem1, "Lose One Small Item")
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