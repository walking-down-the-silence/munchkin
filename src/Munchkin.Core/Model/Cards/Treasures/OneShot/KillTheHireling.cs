using Munchkin.Core.Model.Cards.Treasures.Permanent;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class KillTheHireling : LevelUpTreasure
    {
        public KillTheHireling() : 
            base(MunchkinDeluxeCards.Treasures.KillTheHireling, "Kill The Hireling")
        {
        }

        public override Task Play(Table gameContext)
        {
            var playerWithhirelingCard = gameContext.Players.FirstOrDefault(x => x.Equipped.OfType<Hireling>().Any());
            var hirelingCard = playerWithhirelingCard?.Equipped.FirstOrDefault(x => x is Hireling);
            if (hirelingCard != null)
            {
                hirelingCard.Discard(gameContext);
                return base.Play(gameContext);
            }

            return Task.CompletedTask;
        }
    }
}