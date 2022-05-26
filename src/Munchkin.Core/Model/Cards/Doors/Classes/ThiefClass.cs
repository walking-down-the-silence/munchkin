using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class ThiefClass : ClassCard
    {
        public ThiefClass() :
            base(MunchkinDeluxeCards.Doors.ThiefClass1, "Thief")
        {
            AddAttribute(new ThiefAttribute());

            Theft = new ThiefTheftAction(Owner);
            Backstabbing = new ThiefBackstabbingAction(Owner);
        }

        public ThiefTheftAction Theft { get; }

        public ThiefBackstabbingAction Backstabbing { get; }
    }
}