using Munchkin.Core.Contracts.Actions;
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

        public IAction<Table> AddStrengthAgainstMonster { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            AddStrengthAgainstMonster = new WarriorStrengthBonus1Action();

            return Task.CompletedTask;
        }
    }
}