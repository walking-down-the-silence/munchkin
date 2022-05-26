using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Events;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using System;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class HalflingRace : RaceCard
    {
        public HalflingRace() :
            base(MunchkinDeluxeCards.Doors.HalflingRace1, "Halfling")
        {
            AddAttribute(new HalflingAttribute());
        }

        public Table SellDoublePrice(Table table, ItemCard card)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(card, nameof(card));

            // TODO: Think how to perform check per turn
            if (table.ActionLog.OfType<PlayerCardSoldEvent>().Any())
                throw new PlayerCannotPerformActionException("Plyer cannot sell another item for the double price in the same turn.");

            if (Owner != card.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            var cardSoldEvent = new PlayerCardSoldEvent(Owner.Nickname, card.Code, card.GoldPieces * 2);
            table = table.WithActionEvent(cardSoldEvent);

            return table;
        }

        public Table RerollTheDice(Table table, Card dicardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(dicardCard, nameof(dicardCard));

            if (Owner != dicardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            var diceRollResult = Dice.Roll();
            var diceRollEvent = new PlayerDiceRolledEvent(table.Players.Current.Nickname, diceRollResult);
            table = table.WithActionEvent(diceRollEvent);

            return table;
        }
    }
}