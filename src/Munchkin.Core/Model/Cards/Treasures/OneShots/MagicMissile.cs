using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class MagicMissile : OneShotItemCard
    {
        public MagicMissile() : 
            base(MunchkinDeluxeCards.Treasures.MagicMissile, "Magic Missile", 5, 0, 300)
        {
        }
    }
}