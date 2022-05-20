using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class SleepPotion : OneShotItemCard
    {
        public SleepPotion() :
            base(MunchkinDeluxeCards.Treasures.SleepPotion, "Sleep Potion", 2, 0, 100)
        {
        }
    }
}