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

        public IAction<Table> ReviveTheCard { get; private set; }

        public IAction<Table> AddStrengtAgainstUndead { get; private set; }

        public override Task Play(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            // TODO: Owner here is null because it is not set yet
            ReviveTheCard = new ClericRessurectionAction(Owner);
            AddStrengtAgainstUndead = new ClericTurningAction();

            return Task.CompletedTask;
        }

        public Table ReviveFromDoorDiscard(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            table = table.TakeDoor(out var card);
            Owner.TakeInHand(card);

            return table;
        }

        public Table ReviveFromTreasureDiscard(Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            table = table.TakeTreasure(out var card);
            Owner.TakeInHand(card);

            return table;
        }

        public Table Bonus3AgainstUndead(Table table, Card card)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(card, nameof(card));

            if (Owner != card.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table = table.Discard(card);

            var clericBonus3Event = new ClericClassBonus3AgainstUndeadEvent(Owner.Nickname, card.Code);
            table.ActionLog.Add(clericBonus3Event);

            return table;
        }
    }
}