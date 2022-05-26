using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeSex : CurseCard
    {
        public ChangeSex() : 
            base(MunchkinDeluxeCards.Doors.ChangeSex, "Change Sex")
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
