using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors
{
    public class SuperMunchkin : ClassCard
    {
        public SuperMunchkin() :
            base(MunchkinDeluxeCards.Doors.SuperMunchkin1, "Supermunchkin")
        {
            AddAttribute(new MaximumEquippedClassesAttribute(2));
        }
    }
}