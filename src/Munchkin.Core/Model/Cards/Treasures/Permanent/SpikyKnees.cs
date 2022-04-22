using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SpikyKnees : PermanentItemCard
    {
        public SpikyKnees() : base("Spiky Knees", 1, 0, EItemSize.Small, EWearingType.None, 200)
        {
        }

        public override Task Play(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}