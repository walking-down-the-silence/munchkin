using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using System;
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

            // TODO: Owner here is null because it is not set yet
            RessurectAction = new ClericRessurectionAction(Owner);
            TurningAction = new ClericTurningAction();

            return Task.CompletedTask;
        }

        public Table RessurectFromDoorDiscard(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            table = table.TakeDoor(out var card);
            Owner.TakeInHand(card);

            return table;
        }

        public Table RessurectFromTreasureDiscard(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            table = table.TakeTreasure(out var card);
            Owner.TakeInHand(card);

            return table;
        }

        public Table TurningUndead(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table = table.Discard(discardCard);

            var playerStrengthEvent = new PlayerStrengthBonusChangedEvent(Owner.Nickname, 3);
            table.ActionLog.Add(playerStrengthEvent);

            var clericBonus3Event = new ClericTurningBonus3AgainstUndeadEvent(Owner.Nickname, discardCard.Code);
            table.ActionLog.Add(clericBonus3Event);

            return table;
        }
    }
}