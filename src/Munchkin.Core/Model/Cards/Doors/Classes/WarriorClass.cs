using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class WarriorClass : ClassCard
    {
        public WarriorClass() :
            base(MunchkinDeluxeCards.Doors.WarriorClass1, "Warrior")
        {
            AddAttribute(new WarriorAttribute());

            Berserking = new WarriorBerserkingAction(Owner);
        }

        public WarriorBerserkingAction Berserking { get; }
    }
}