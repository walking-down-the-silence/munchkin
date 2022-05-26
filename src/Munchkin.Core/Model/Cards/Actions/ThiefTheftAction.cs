using Munchkin.Core.Contracts;
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
    public sealed class ThiefTheftAction : DynamicAction
    {
        public ThiefTheftAction(Player owner) :
            base(ThiefClass.Theft, "Theft")
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public Player Owner { get; }

        public Card DiscardCard { get; set; }

        public ItemCard TheftCard { get; set; }

        protected override bool OnCanExecute(Table table)
        {
            return DiscardCard is not null
                && TheftCard is not null
                && Owner == DiscardCard.Owner
                && TheftCard.ItemSize != EItemSize.Small;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return Theft(table, DiscardCard, TheftCard).Unit();
        }

        public Table Theft(Table table, Card discardCard, ItemCard theftCard)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(discardCard, nameof(discardCard));
            ArgumentNullException.ThrowIfNull(theftCard, nameof(theftCard));

            if (Owner != discardCard.Owner)
                throw new PlayerDoesNotOwnTheCardException();

            if (theftCard.ItemSize != EItemSize.Small)
                throw new PlayerCannotPerformActionException("Player cannot use 'Theft' ability and steal an item (only small items can be stolen).");

            var diceRollResult = Dice.Roll();
            var playerDiceRolledEvent = new PlayerDiceRolledEvent(Owner.Nickname, diceRollResult);
            table = table.WithActionEvent(playerDiceRolledEvent);

            var berserkingEvent = new ThiefTheftActionEvent(Owner.Nickname, discardCard.Code);
            table = table.WithActionEvent(berserkingEvent);

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