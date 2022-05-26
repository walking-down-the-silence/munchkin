using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class WarriorClass : ClassCard
    {
        public WarriorClass() :
            base(MunchkinDeluxeCards.Doors.WarriorClass1, "Warrior")
        {
            AddAttribute(new WarriorAttribute());
        }

        public override Task Play(Table table)
        {
            Berserking = new WarriorBerserkingAction(Owner);

            return base.Play(table);
        }

        public WarriorBerserkingAction Berserking { get; private set; }
    }
}