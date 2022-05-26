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

        public override Task Play(Table table)
        {
            CharmSpell = new WizardCharmSpellAction(Owner);
            FlightSpell = new WizardFlightSpellAction(Owner);

            return base.Play(table);
        }

        public WizardCharmSpellAction CharmSpell { get; private set; }

        public WizardFlightSpellAction FlightSpell { get; private set; }
    }
}