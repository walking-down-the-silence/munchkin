using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class TransferralPotion : OneShotItemCard
    {
        public TransferralPotion() :
            base(MunchkinDeluxeCards.Treasures.TransferralPotion, "Transferral Potion", 0, 0, 300)
        {
        }
    }
}