using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class DuckOfDoom : CurseCard
    {
        public DuckOfDoom() : 
            base(MunchkinDeluxeCards.Doors.DuckOfDoom, "Duck Of Doom")
        {
        }

        public override Task BadStuff(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            table.Players.Current.LevelDown();
            table.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}