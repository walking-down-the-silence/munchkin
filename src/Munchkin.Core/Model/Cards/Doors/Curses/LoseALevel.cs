using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseALevel : CurseCard
    {
        public LoseALevel() : base("Loose A Level")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}