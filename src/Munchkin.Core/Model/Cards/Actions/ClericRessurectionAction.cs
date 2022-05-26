using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Extensions.Threading;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Actions
{
    public record ClericRessurectionAction(Player Owner) :
        DynamicAction(ClericClass.Ressurection, "Ressurection", "Take From Discard")
    {
        public Card DiscardCard { get; }

        public bool IsRessurectingFromDoorDeck { get; }

        protected override bool OnCanExecute(Table table)
        {
            return DiscardCard is not null
                && Owner == DiscardCard.Owner;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return IsRessurectingFromDoorDeck
                ? RessurectFromDoorDiscard(table, DiscardCard).Unit()
                : RessurectFromTreasureDiscard(table, DiscardCard).Unit();
        }

        public Table RessurectFromDoorDiscard(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table = table.Discard(discardCard);
            table = table.TakeDoor(out var card);
            Owner.TakeInHand(card);

            var ressurectEvent = new ClericRessurectActionEvent(Owner.Nickname, discardCard.Code, card.Code);
            table = table.WithActionEvent(ressurectEvent);

            return table;
        }

        public Table RessurectFromTreasureDiscard(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table = table.Discard(discardCard);
            table = table.TakeTreasure(out var card);
            Owner.TakeInHand(card);

            var ressurectEvent = new ClericRessurectActionEvent(Owner.Nickname, discardCard.Code, card.Code);
            table = table.WithActionEvent(ressurectEvent);

            return table;
        }
    }
}