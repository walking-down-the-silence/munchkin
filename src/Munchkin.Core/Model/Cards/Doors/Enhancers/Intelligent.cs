using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Doors.Enhancers
{
    public sealed class Intelligent : EnhancerCard
    {
        public Intelligent() :
            base(MunchkinDeluxeCards.Doors.Intelligent, "Intelligent", 5, 1)
        {
        }
    }
}