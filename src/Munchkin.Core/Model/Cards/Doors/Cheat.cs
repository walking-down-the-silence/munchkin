using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class Cheat : SpecialCard
    {
        public Cheat() :
            base(MunchkinDeluxeCards.Doors.Cheat, "Cheat")
        {
            AddAttribute(new CheatAttribute());
        }
    }
}