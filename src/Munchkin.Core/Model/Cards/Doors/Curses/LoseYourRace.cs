using Munchkin.Core.Contracts.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseYourRace : CurseCard
    {
        public LoseYourRace() :
            base(MunchkinDeluxeCards.Doors.LoseYourRace, "Lose Your Race")
        {
        }

        public override Task BadStuff(Table context)
        {
            var races = context.Players.Current.Equipped
                .OfType<RaceCard>()
                .ToList();

            if (races.Count > 1)
            {
                // select which one to discard
            }
            else
            {
                races.FirstOrDefault()?.Discard(context);
            }

            return Task.CompletedTask;
        }
    }
}