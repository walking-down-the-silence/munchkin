using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class OneShotItemCard : ItemCard
    {
        protected OneShotItemCard(string code, string title, int strength, int runAwayBonus, int goldPieces)
            : base(code, title, strength, runAwayBonus, goldPieces, EItemSize.Small)
        {
        }

        public override Task Play(Table context) => Task.CompletedTask;
    }
}