using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SandalsOfProtection : WearingCard
    {
        public SandalsOfProtection() : 
            base(MunchkinDeluxeCards.Treasures.SandalsOfProtection, "Sandals Of Protection", 0, 0, EItemSize.Small, EWearingType.Footgear, 700)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}