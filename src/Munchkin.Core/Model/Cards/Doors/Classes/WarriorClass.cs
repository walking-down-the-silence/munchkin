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
    public sealed class WarriorClass : ClassCard
    {
        public WarriorClass() :
            base(MunchkinDeluxeCards.Doors.WarriorClass1, "Warrior")
        {
            AddAttribute(new WarriorAttribute());
        }

        public IAction<Table> BerserkingAction { get; private set; }

        public override Task Play(Table table)
        {
            BerserkingAction = new WarriorBerserkingAction();

            return Task.CompletedTask;
        }

        public Table Berserking(Table table, Card discardCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            table.Discard(discardCard);

            var playerStrengthEvent = new PlayerStrengthBonusChangedEvent(Owner.Nickname, 1);
            table.ActionLog.Add(playerStrengthEvent);

            var clericBonus3Event = new WarriorBerserkingBonus1Event(Owner.Nickname, discardCard.Code);
            table.ActionLog.Add(clericBonus3Event);

            return table;
        }
    }
}