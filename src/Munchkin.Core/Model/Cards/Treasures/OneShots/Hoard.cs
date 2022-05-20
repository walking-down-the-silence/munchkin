using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class Hoard : OneShotItemCard
    {
        public Hoard() : 
            base(MunchkinDeluxeCards.Treasures.Hoard, "Hoard", 0, 0, 0)
        {
        }
    }
}