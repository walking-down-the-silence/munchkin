using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class WizardClass : ClassCard
    {
        public WizardClass() :
            base(MunchkinDeluxeCards.Doors.WizardClass1, "Wizard")
        {
            AddAttribute(new WizardAttribute());
        }

        public IAction<Table> FleeMonster { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            FleeMonster = new WizardFleeMonsterAction();

            return Task.CompletedTask;
        }
    }
}