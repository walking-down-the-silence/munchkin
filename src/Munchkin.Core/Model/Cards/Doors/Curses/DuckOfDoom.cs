using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class DuckOfDoom : CurseCard
    {
        public DuckOfDoom() : 
            base(MunchkinDeluxeCards.Doors.DuckOfDoom, "Duck Of Doom")
        {
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