using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class MaceOfSharpness : WearingCard
    {
        public MaceOfSharpness() :
            base(MunchkinDeluxeCards.Treasures.MaceOfSharpness, "Mace Of Sharpness", 4, 0, EItemSize.Small, EWearingType.OneHanded, 600)
        {
            AddRestriction(new UsableByClericOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}