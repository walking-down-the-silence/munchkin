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
    public sealed class ThiefClass : ClassCard
    {
        public ThiefClass() :
            base(MunchkinDeluxeCards.Doors.ThiefClass1, "Thief")
        {
            AddAttribute(new ThiefAttribute());
        }

        public IAction<Table> TheftAction { get; private set; }

        public IAction<Table> BackstabbingAction { get; private set; }

        public override Task Play(Table table)
        {
            TheftAction = new ThiefTheftAction();
            BackstabbingAction = new ThiefBackstabbingAction();

            return Task.CompletedTask;
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
            table.ActionLog.Add(playerStrengthEvent);

            var berserkingEvent = new ThiefBackstabbingActionEvent(Owner.Nickname, discardCard.Code);
            table.ActionLog.Add(berserkingEvent);

            return table;
        }

        public Table Theft(Table table, Card discardCard, ItemCard theftCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));
            ArgumentNullException.ThrowIfNull(theftCard, nameof(theftCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            if (theftCard.ItemSize != Contracts.EItemSize.Small)
                throw new PlayerCannotPerformActionException("Player cannot use 'Theft' ability and steal an item (only small items can be stolen).");

            var diceRollResult = Dice.Roll();
            var playerDiceRolledEvent = new PlayerDiceRolledEvent(Owner.Nickname, diceRollResult);
            table.ActionLog.Add(playerDiceRolledEvent);

            var berserkingEvent = new ThiefTheftActionEvent(Owner.Nickname, discardCard.Code);
            table.ActionLog.Add(berserkingEvent);

            if (diceRollResult >= 4)
            {
                table.Discard(discardCard);
                Owner.TakeInHand(theftCard);
            }
            else
            {
                Owner.LevelDown();
            }

            return table;
        }
    }
}