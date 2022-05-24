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

            if (table.ActionLog.OfType<WarriorBerserkingBonus1Event>().Count() >= 3)
                throw new PlayerCannotPerformActionException("Player cannot use 'Berserking' ability, because it was used maximum times (3 times per turn).");

            table.Discard(discardCard);

            var playerStrengthEvent = new PlayerStrengthBonusChangedEvent(Owner.Nickname, 1);
            table.ActionLog.Add(playerStrengthEvent);

            var berserkingEvent = new WarriorBerserkingBonus1Event(Owner.Nickname, discardCard.Code);
            table.ActionLog.Add(berserkingEvent);

            return table;
        }
    }
}