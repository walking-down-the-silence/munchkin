using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class HelmOfCourage : PermanentItemCard
    {
        public HelmOfCourage() : base("Helm Of Courage", 1, 0, EItemSize.Small, EWearingType.Headgear, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}