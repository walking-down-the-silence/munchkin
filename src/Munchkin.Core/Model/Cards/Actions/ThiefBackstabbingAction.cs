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
    public sealed class ThiefBackstabbingAction : DynamicAction
    {
        public ThiefBackstabbingAction(Player owner) :
            base(ThiefClass.Backstabbing, "Backstabbing")
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public Player Owner { get; }

        public Card DiscardCard { get; set; }

        protected override bool OnCanExecute(Table table)
        {
            return DiscardCard is not null
                && Owner == DiscardCard.Owner
                && table.ActionLog.OfType<ThiefBackstabbingActionEvent>().Count() < 2;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return Backstabbing(table, DiscardCard).Unit();
        }

        public Table Backstabbing(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (table.ActionLog.OfType<ThiefBackstabbingActionEvent>().Count() >= 2)
                throw new PlayerCannotPerformActionException("Player cannot use 'Backstabbing' ability, because it was used maximum times (maximum 1 per each player in combat).");

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table.Discard(discardCard);

            var playerStrengthEvent = new PlayerStrengthBonusChangedEvent(Owner.Nickname, -2);
            table = table.WithActionEvent(playerStrengthEvent);

            var berserkingEvent = new ThiefBackstabbingActionEvent(Owner.Nickname, discardCard.Code);
            table = table.WithActionEvent(berserkingEvent);

            return table;
        }
    }
}