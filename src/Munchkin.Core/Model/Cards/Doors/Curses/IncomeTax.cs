using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Extensions.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class IncomeTax : CurseCard
    {
        public IncomeTax() :
            base(MunchkinDeluxeCards.Doors.IncomeTax, "Income Tax")
        {
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            // TODO: Add this action to the stack of player actions
            var playerBadStuffAction = new DiscardSingleItemAction((table, card) =>
            {
                var goldPieces = card.GoldPieces;
                table = table.Discard(card);

                var otherPlayersActions = table.Players
                    .Where(current => current != player)
                    .Select(current => new DiscardMultipleItemsAction((table, cards) => TakeBadStuff(table, goldPieces, current, cards)));

                return table;
            });


            return table;
        }

        private static Table TakeBadStuff(Table table, int goldPieces, Player player, IReadOnlyCollection<ItemCard> cards)
        {
            return cards.Sum(x => x.GoldPieces) >= goldPieces
                ? TakeBadStuffOption1(table, cards)
                : TakeBadStuffOption2(table, player);
        }

        private static Table TakeBadStuffOption1(Table table, IEnumerable<ItemCard> items)
        {
            return DiscardAll(table, items);
        }

        private static Table TakeBadStuffOption2(Table table, Player player)
        {
            player.LevelDown();
            return DiscardAll(table, player.AllCards().OfType<ItemCard>());
        }

        private static Table DiscardAll(Table table, IEnumerable<ItemCard> items)
        {
            return items.Aggregate(table, (result, item) => result.Discard(item));
        }
    }

    public delegate Table DiscardSingleItemAction(Table table, ItemCard card);

    public delegate Table DiscardMultipleItemsAction(Table table, IReadOnlyCollection<ItemCard> cards);
}