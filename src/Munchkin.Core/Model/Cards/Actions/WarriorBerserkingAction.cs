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
    public record WarriorBerserkingAction(Player Owner) :
        DynamicAction(WarriorClass.Berserking, "Berserking", "Bonus (+1)")
    {
        public Card DiscardCard { get; }

        protected override bool OnCanExecute(Table table)
        {
            return DiscardCard is not null
                && Owner == DiscardCard.Owner
                && table.ActionLog.OfType<WarriorBerserkingBonus1Event>().Count() < 3;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return Berserking(table, DiscardCard).Unit();
        }

        public Table Berserking(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            if (table.ActionLog.OfType<WarriorBerserkingBonus1Event>().Count() >= 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Berserking' ability, because it was used maximum times (3 times per turn).");

            table.Discard(discardCard);

            var playerStrengthEvent = new PlayerStrengthBonusChangedEvent(Owner.Nickname, 1);
            table = table.WithActionEvent(playerStrengthEvent);

            var berserkingEvent = new WarriorBerserkingBonus1Event(Owner.Nickname, discardCard.Code);
            table = table.WithActionEvent(berserkingEvent);

            return table;
        }
    }
}