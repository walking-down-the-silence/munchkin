using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class RapierOfUnfairness : WearingCard
    {
        public RapierOfUnfairness() :
            base(MunchkinDeluxeCards.Treasures.RapierOfUnfairness, "Rapier Of Unfairness", 3, 0, EItemSize.Small, EWearingType.OneHanded, 600)
        {
            AddRestriction(new UsableByElfOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}