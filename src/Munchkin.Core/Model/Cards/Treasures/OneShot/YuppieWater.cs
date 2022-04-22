using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Treasures.OneShot
{
    public sealed class YuppieWater : OneShotItemCard
    {
        public YuppieWater() : base("Yuppie Water", 2, 0, 100)
        {
            AddProperty(new ElfOnlyRestriction());
        }
    }
}