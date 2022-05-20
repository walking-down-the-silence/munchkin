using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class LoadedDie : OneShotItemCard
    {
        public LoadedDie() : 
            base(MunchkinDeluxeCards.Treasures.LoadedDie, "Loaded Die", 0, 6, 300)
        {
        }
    }
}