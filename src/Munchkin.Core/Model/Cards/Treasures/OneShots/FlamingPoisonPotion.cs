using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class FlamingPoisonPotion : OneShotItemCard
    {
        public FlamingPoisonPotion() : 
            base(MunchkinDeluxeCards.Treasures.FlamingPoisonPotion, "Flaming Poison Potion", 3, 0, 100)
        {
        }
    }
}