using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class YuppieWater : OneShotItemCard
    {
        public YuppieWater() :
            base(MunchkinDeluxeCards.Treasures.YuppieWater, "Yuppie Water", 2, 0, 100)
        {
            AddRestriction(new UsableByElfOnlyRestriction());
        }
    }
}