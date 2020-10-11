using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class BucklerOfSwashing : PermanentItemCard
    {
        public BucklerOfSwashing() : base("Buckler Of Swashing", 2, 0, EItemSize.Small, EWearingType.OneHanded, 400)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}