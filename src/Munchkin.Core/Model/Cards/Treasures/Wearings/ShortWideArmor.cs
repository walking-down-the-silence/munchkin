using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class ShortWideArmor : WearingCard
    {
        public ShortWideArmor() : 
            base(MunchkinDeluxeCards.Treasures.ShortWideArmor, "Short Wide Armor", 3, 0, EItemSize.Small, EWearingType.Armor, 400)
        {
            AddRestriction(new UsableByDwarfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}