using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class Hireling : PermanentItemCard
    {
        public Hireling() :
            base(MunchkinDeluxeCards.Treasures.Hireling, "Hireling", 1, 6, EItemSize.Small, EWearingType.None, 0)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}