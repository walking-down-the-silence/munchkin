using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class BroadSword : WearingCard
    {
        public BroadSword() :
            base(MunchkinDeluxeCards.Treasures.BroadSword, "Broad Sword", 3, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
            AddRestriction(new UsableByFemaleOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}