using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Events;
using Munchkin.Core.Model.Exceptions;
using Munchkin.Extensions.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Munchkin.Core.Model.Cards.MunchkinDeluxeCards;

namespace Munchkin.Core.Model.Cards.Actions
{
    public sealed class HalflingSellDoublePriceAction : DynamicAction
    {
        public HalflingSellDoublePriceAction(Player owner) :
            base(HalflingRace.SellDoublePrice, "Sell Double Price")
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public Player Owner { get; }

        public ItemCard SellCard { get; set; }

        protected override bool OnCanExecute(Table table)
        {
            return SellCard is not null
                && Owner == SellCard.Owner
                && !table.ActionLog.OfType<PlayerCardSoldEvent>().Any();
        }

        protected override Task<Table> OnExecuteAsync(Table table)
        {
            return SellDoublePrice(table, SellCard).Unit();
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
    }
}
