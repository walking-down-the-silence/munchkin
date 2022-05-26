using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public class LevelUpTreasure : OneShotItemCard
    {
        public LevelUpTreasure(string code, string title) : 
            base(code, title, 0, 0, 0)
        {
        }

        public override Task Play(Table table)
        {
            if (!Owner.WillBeWinning(table.WinningLevel))
            {
                Owner.LevelUp();
            }

            return Task.CompletedTask;
        }
    }
}