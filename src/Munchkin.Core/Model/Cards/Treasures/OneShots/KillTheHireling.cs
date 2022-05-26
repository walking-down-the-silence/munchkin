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

        public override Task Play(Table table)
        {
            var playerWithhirelingCard = table.Players.FirstOrDefault(x => x.Equipped.OfType<Hireling>().Any());
            var hirelingCard = playerWithhirelingCard?.Equipped.FirstOrDefault(x => x is Hireling);
            
            if (hirelingCard != null)
            {
                table = table.Discard(hirelingCard);
                Owner?.LevelUp();

                return base.Play(table);
            }

            return Task.CompletedTask;
        }
    }
}