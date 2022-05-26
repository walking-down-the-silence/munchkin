using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Extensions.Threading;
using System;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Cards.Actions
{
    public sealed class HalflingRerollTheDiceAction : DynamicAction
    {
        public HalflingRerollTheDiceAction(Player owner) :
            base(HalflingRace.RerollTheDice, "Reroll The Dice")
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public Player Owner { get; }

        public Card DiscardCard { get; set; }

        protected override bool OnCanExecute(Table table)
        {
            return DiscardCard is not null
                && Owner == DiscardCard.Owner;
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return RerollTheDice(table, DiscardCard).Unit();
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
