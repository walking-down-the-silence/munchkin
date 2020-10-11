using Munchkin.Core.Model.Cards;
using Munchkin.Engine.Original.CardProperties;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class YuppieWater : OneShotItemCard
    {
        public YuppieWater() : base("Yuppie Water", 2, 0, 100)
        {
            AddProperty(new ElfOnlyRestriction());
        }
    }
}