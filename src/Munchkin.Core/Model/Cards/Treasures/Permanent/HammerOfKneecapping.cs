using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class HammerOfKneecapping : PermanentItemCard
    {
        public HammerOfKneecapping() :
            base(MunchkinDeluxeCards.Treasures.HammerOfKneecapping, "Hammer Of Kneecapping", 4, 0, EItemSize.Small, EWearingType.OneHanded, 600)
        {
            AddRestriction(new UsableByDwarfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}