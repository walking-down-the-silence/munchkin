using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseYourRace : CurseCard
    {
        public LoseYourRace() : base("Lose Your Race")
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