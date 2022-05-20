using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class DuckOfDoom : CurseCard
    {
        public DuckOfDoom() : 
            base(MunchkinDeluxeCards.Doors.DuckOfDoom, "Duck Of Doom")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.LevelDown();
            context.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}