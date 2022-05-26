using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class CheeseGreaterOfPeace : WearingCard
    {
        public CheeseGreaterOfPeace() : 
            base(MunchkinDeluxeCards.Treasures.CheeseGreaterOfPeace, "Cheese Greater Of Peace", 3, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
            AddRestriction(new UsableByClericOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}