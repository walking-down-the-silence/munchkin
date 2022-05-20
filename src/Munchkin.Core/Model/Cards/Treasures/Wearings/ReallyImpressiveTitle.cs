using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class ReallyImpressiveTitle : WearingCard
    {
        public ReallyImpressiveTitle() : 
            base(MunchkinDeluxeCards.Treasures.ReallyImpressiveTitle, "Really Impressive Title", 3, 0, EItemSize.Small, EWearingType.None, 0)
        {
        }

        public override Task Play(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}