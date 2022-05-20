using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class Stepladder : PermanentItemCard
    {
        public Stepladder() :
            base(MunchkinDeluxeCards.Treasures.Stepladder, "Stepladder", 3, 0, EItemSize.Big, EWearingType.None, 400)
        {
            AddRestriction(new UsableByHalflingOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}