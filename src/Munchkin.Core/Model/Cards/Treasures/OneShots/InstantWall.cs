using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class InstantWall : OneShotItemCard
    {
        public InstantWall() : 
            base(MunchkinDeluxeCards.Treasures.InstantWall, "InstantWall", 0, 6, 300)
        {
        }
    }
}