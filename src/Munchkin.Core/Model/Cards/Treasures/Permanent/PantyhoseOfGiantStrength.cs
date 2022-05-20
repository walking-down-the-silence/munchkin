using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class PantyhoseOfGiantStrength : PermanentItemCard
    {
        public PantyhoseOfGiantStrength() : 
            base(MunchkinDeluxeCards.Treasures.PantyhoseOfGiantStrength, "Pantyhose Of Giant Strength", 3, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddRestriction(new NotUsableByWarriorRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}