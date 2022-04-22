using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
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