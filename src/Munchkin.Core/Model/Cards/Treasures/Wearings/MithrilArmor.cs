using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class MithrilArmor : WearingCard
    {
        public MithrilArmor() : 
            base(MunchkinDeluxeCards.Treasures.MithrilArmor, "Mithril Armor", 3, 0, EItemSize.Big, EWearingType.Armor, 600)
        {
            //AddAttribute(new NotUsableByWizardRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}