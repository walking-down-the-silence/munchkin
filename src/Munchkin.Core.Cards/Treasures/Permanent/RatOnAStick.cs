using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class RatOnAStick : PermanentItemCard
    {
        public RatOnAStick() : base("Rat On A Stick", 1, 0, EItemSize.Small, EWearingType.OneHanded, 0)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}