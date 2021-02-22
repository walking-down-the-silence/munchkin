using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
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