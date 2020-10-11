using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class TubaOfCharm : PermanentItemCard
    {
        public TubaOfCharm() : base("Tuba Of Charm", 0, 3, EItemSize.Big, EWearingType.OneHanded, 300)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}