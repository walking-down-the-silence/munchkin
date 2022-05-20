using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class StaffOfNapalm : PermanentItemCard
    {
        public StaffOfNapalm() :
            base(MunchkinDeluxeCards.Treasures.StaffOfNapalm, "Staff Of Napalm", 5, 0, EItemSize.Small, EWearingType.OneHanded, 800)
        {
            AddRestriction(new UsableByWizardOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}