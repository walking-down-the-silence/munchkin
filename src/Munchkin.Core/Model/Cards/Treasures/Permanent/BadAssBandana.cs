using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class BadAssBandana : PermanentItemCard
    {
        public BadAssBandana() :
            base(MunchkinDeluxeCards.Treasures.BadAssBandana, "Bad-Ass Bandana", 3, 0, EItemSize.Small, EWearingType.Headgear, 400)
        {
            AddRestriction(new UsableByHumanOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}