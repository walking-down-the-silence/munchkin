using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class DaggerOfTreachery : PermanentItemCard
    {
        public DaggerOfTreachery() :
            base(MunchkinDeluxeCards.Treasures.DaggerOfTreachery, "Dagger Of Treachery", 3, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
            AddRestriction(new UsableByThiefOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}