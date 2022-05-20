using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class HelmOfCourage : WearingCard
    {
        public HelmOfCourage() : 
            base(MunchkinDeluxeCards.Treasures.HelmOfCourage, "Helm Of Courage", 1, 0, EItemSize.Small, EWearingType.Headgear, 200)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}