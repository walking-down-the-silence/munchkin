using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Doors.Enhancers
{
    public sealed class Humongous : EnhancerCard
    {
        public Humongous() : 
            base(MunchkinDeluxeCards.Doors.Humongous, "Humongous", 10, 2)
        {
        }
    }
}