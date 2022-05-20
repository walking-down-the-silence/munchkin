using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseALevel : CurseCard
    {
        public LoseALevel() : 
            base(MunchkinDeluxeCards.Doors.LoseALevel1, "Loose A Level")
        {
        }

        public override Task BadStuff(Table context)
        {
            context.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}