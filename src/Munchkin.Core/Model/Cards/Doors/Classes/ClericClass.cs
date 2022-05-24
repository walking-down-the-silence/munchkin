using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Classes
{
    public sealed class ClericClass : ClassCard
    {
        public ClericClass() :
            base(MunchkinDeluxeCards.Doors.ClericClass1, "Cleric")
        {
            AddAttribute(new ClericAttribute());
        }

        public IAction<Table> RessurectAction { get; private set; }

        public IAction<Table> TurningAction { get; private set; }

        public override Task Play(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            RessurectAction = new ClericRessurectionAction(Owner);
            TurningAction = new ClericTurningAction();

            return Task.CompletedTask;
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
            table.ActionLog.Add(ressurectEvent);

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
            table.ActionLog.Add(ressurectEvent);

            return table;
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
            table.ActionLog.Add(playerStrengthEvent);

            var turningEvent = new ClericTurningActionEvent(Owner.Nickname, discardCard.Code);
            table.ActionLog.Add(turningEvent);

            return table;
        }
    }
}