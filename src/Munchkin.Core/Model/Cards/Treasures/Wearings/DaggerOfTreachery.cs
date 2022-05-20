using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class DaggerOfTreachery : WearingCard
    {
        public DaggerOfTreachery() :
            base(MunchkinDeluxeCards.Treasures.DaggerOfTreachery, "Dagger Of Treachery", 3, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
            AddRestriction(new UsableByThiefOnlyRestriction());
        }
    }
}