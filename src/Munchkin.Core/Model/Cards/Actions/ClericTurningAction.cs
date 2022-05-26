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
    public record ClericTurningAction(Player Owner) :
        DynamicAction(ClericClass.Turning, "Turning", "Bonus +3")
    {
        public Card DiscardCard { get; }

        protected override bool OnCanExecute(Table table)
        {
            return DiscardCard is not null
                && Owner == DiscardCard.Owner
                && table.ActionLog.OfType<ClericTurningActionEvent>().Count() < 3;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return TurningUndead(table, DiscardCard).Unit();
        }

        public Table TurningUndead(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (table.ActionLog.OfType<ClericTurningActionEvent>().Count() >= 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Turning' ability, because it was used maximum times (3 times per turn).");

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table = table.Discard(discardCard);

            var playerStrengthEvent = new PlayerStrengthBonusChangedEvent(Owner.Nickname, 3);
            table = table.WithActionEvent(playerStrengthEvent);

            var turningEvent = new ClericTurningActionEvent(Owner.Nickname, discardCard.Code);
            table = table.WithActionEvent(turningEvent);

            return table;
        }
    }
}
