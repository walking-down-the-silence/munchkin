using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Cheat : SpecialCard
    {
        public Cheat() :
            base(MunchkinDeluxeCards.Doors.Cheat, "Cheat")
        {
        }
    }
}