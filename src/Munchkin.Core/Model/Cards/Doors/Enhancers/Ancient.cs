using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Doors.Enhancers
{
    public sealed class Ancient : EnhancerCard
    {
        public Ancient() : 
            base(MunchkinDeluxeCards.Doors.Ancient, "Ancient", 10, 2)
        {
        }
    }
}