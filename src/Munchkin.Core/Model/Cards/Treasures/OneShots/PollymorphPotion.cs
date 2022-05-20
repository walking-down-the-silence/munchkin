using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class PollymorphPotion : OneShotItemCard
    {
        public PollymorphPotion() : 
            base(MunchkinDeluxeCards.Treasures.PollymorphPotion, "Pollymorph Potion", 0, 0, 1300)
        {
        }
    }
}