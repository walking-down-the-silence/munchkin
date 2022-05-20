using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class PotionOfHalitosis : OneShotItemCard
    {
        public PotionOfHalitosis() : 
            base(MunchkinDeluxeCards.Treasures.PotionOfHalitosis, "Potion Of Halitosis", 2, 0, 100)
        {
        }
    }
}