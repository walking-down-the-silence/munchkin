using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class InvisibilityPotion : OneShotItemCard
    {
        public InvisibilityPotion() : 
            base(MunchkinDeluxeCards.Treasures.InvisibilityPotion, "Invisibility Potion", 0, 6, 200)
        {
        }
    }
}