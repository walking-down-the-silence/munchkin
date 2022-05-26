using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class WizardClass : ClassCard
    {
        public WizardClass() :
            base(MunchkinDeluxeCards.Doors.WizardClass1, "Wizard")
        {
            AddAttribute(new WizardAttribute());

            CharmSpell = new WizardCharmSpellAction(Owner);
            FlightSpell = new WizardFlightSpellAction(Owner);
        }

        public WizardCharmSpellAction CharmSpell { get; }

        public WizardFlightSpellAction FlightSpell { get; }
    }
}