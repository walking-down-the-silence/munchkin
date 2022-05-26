using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Extensions.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    public record WizardFlightSpellAction(Player Owner) :
        DynamicAction(WizardClass.FlightSpell, "Flight Spell", "Get +1 To Run Away")
    {
        public Card DicardCard { get; }

        protected override bool OnCanExecute(Table table)
        {
            return DicardCard is not null
                && Owner == DicardCard.Owner
                && table.ActionLog.OfType<CombatRunAwayBonusEvent>().Count() < 3;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return FlightSpell(table, DicardCard).Unit();
        }

        public Table FlightSpell(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            if (table.ActionLog.OfType<CombatRunAwayBonusEvent>().Count() >= 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Flight Spell' ability, because it was used maximum times (3 per turn).");

            var runAwayBonus = new CombatRunAwayBonusEvent(Owner.Nickname, 1);
            table = table.WithActionEvent(runAwayBonus);

            var flightSpellEvent = new WizardFlightSpellActionEvent(Owner.Nickname, discardCard.Code);
            table = table.WithActionEvent(flightSpellEvent);

            return table;
        }
    }
}