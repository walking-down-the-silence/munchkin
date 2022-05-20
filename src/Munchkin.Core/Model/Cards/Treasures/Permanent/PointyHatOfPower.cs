using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class PointyHatOfPower : PermanentItemCard
    {
        public PointyHatOfPower() : 
            base(MunchkinDeluxeCards.Treasures.PointyHatOfPower, "Pointy Hat Of Power", 3, 0, EItemSize.Small, EWearingType.Headgear, 400)
        {
            AddRestriction(new UsableByWizardOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}