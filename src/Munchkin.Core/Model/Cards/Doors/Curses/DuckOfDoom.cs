using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class DuckOfDoom : CurseCard
    {
        public DuckOfDoom() : base("Duck Of Doom")
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