using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class HugeRock : PermanentItemCard
    {
        public HugeRock() :
            base(MunchkinDeluxeCards.Treasures.HugeRock, "Huge Rock", 3, 0, EItemSize.Big, EWearingType.TwoHanded, 0)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}