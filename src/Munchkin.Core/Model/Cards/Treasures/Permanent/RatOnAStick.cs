using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class RatOnAStick : PermanentItemCard
    {
        public RatOnAStick() : 
            base(MunchkinDeluxeCards.Treasures.RatOnAStick, "Rat On A Stick", 1, 0, EItemSize.Small, EWearingType.OneHanded, 0)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}