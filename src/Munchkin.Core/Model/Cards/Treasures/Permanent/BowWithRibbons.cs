using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class BowWithRibbons : PermanentItemCard
    {
        public BowWithRibbons() :
            base(MunchkinDeluxeCards.Treasures.BowWithRibbons, "Bow With Ribbons", 4, 0, EItemSize.Small, EWearingType.TwoHanded, 800)
        {
            AddRestriction(new UsableByElfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}