using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class QDice : OneShotItemCard
    {
        public QDice() :
            base(MunchkinDeluxeCards.Treasures.QDice, "Q-Dice", 0, 0, 1000)
        {
        }
    }
}