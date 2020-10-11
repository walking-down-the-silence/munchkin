using System.Threading.Tasks;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Treasures
{
    public class LevelUpTreasure : OneShotItemCard
    {
        public LevelUpTreasure(string title) : base(title, 0, 0, 0)
        {
        }

        public override Task Play(Table gameContext)
        {
            if (!Owner.WillBecomeWinner(gameContext.WinningLevel))
            {
                Owner.LevelUp();
            }

            return Task.CompletedTask;
        }
    }
}