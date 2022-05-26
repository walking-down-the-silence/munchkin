using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseALevel : CurseCard
    {
        public LoseALevel() :
            base(MunchkinDeluxeCards.Doors.LoseALevel1, "Loose A Level")
        {
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