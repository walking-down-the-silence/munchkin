using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class TrulyObmoxiousCurse : CurseCard
    {
        public TrulyObmoxiousCurse() :
            base(MunchkinDeluxeCards.Doors.TrulyObmoxiousCurse, "Truly Obmoxious Curse")
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