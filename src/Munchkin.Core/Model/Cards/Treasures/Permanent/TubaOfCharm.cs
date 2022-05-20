using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class TubaOfCharm : PermanentItemCard
    {
        public TubaOfCharm() : 
            base(MunchkinDeluxeCards.Treasures.TubaOfCharm, "Tuba Of Charm", 0, 3, EItemSize.Big, EWearingType.OneHanded, 300)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}