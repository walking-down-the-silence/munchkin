using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Exceptions;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class TableExtensions
    {
        /// <summary>
        /// Gets if any of the players has won the game.
        /// </summary>
        public static bool IsGameOver(this Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            return table.Players.Any(x => x.Level >= table.WinningLevel);
        }

        /// <summary>
        /// Gets if the table is empty and has no players besides it.
        /// </summary>
        /// <param name="table">The table to chak against.</param>
        public static bool IsEmpty(this Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            return !table.Players.Any();
        }

        /// <summary>
        /// Closes the table for joining/leaving and shuffles the decks.
        /// </summary>
        /// <returns></returns>
        public static Table Setup(this Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            // NOTE: Divide the cards into the Door deck and the Treasure deck. Shuffle both decks.
            // NOTE: Deal four cards from each deck to each player.
            table = table with
            {
                IsClosed = true,
                DoorsCardDeck = table.DoorsCardDeck.Shuffle(),
                TreasureCardDeck = table.TreasureCardDeck.Shuffle()
            };

            table = table.Players.Aggregate(table, (table, player) => table.DealCards(player));

            return table;
        }

        /// <summary>
        /// Defines an action that moves the turn to the next player.
        /// </summary>
        /// <returns>Returns an updated isntance of the table after the turn has moved to another player.</returns>
        public static Table NextTurn(this Table table)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));

            var currentPlayer = table.Players.Current;

            if (currentPlayer.YourHand.Count > currentPlayer.GetMaximumCardsInHand())
                throw new PlayerHasTooManyCardsInHandException();

            var doorCardsToDiscard = table.DungeonCards.OfType<DoorsCard>();
            var treasureCardsToDiscard = table.DungeonCards.OfType<TreasureCard>();

            table = table with
            {
                DiscardedDoorsCards = table.DiscardedDoorsCards.PutRange(doorCardsToDiscard),
                DiscardedTreasureCards = table.DiscardedTreasureCards.PutRange(treasureCardsToDiscard),
                DungeonCards = ImmutableArray<Card>.Empty
            };

            // NOTE: When the next player begins his turn, your new character appears and can
            // help others in combat with his Level and Class or Race abilities... but you
            // have no cards, unless you receive Charity or gifts from other players.
            if (currentPlayer.IsDead())
                currentPlayer.Revive();

            var nextPlayer = table.Players.Next();

            // NOTE: On your next turn, start by drawing four face-down cards from each deck
            // and playing any legal cards you want to, just as when you started the game.
            if (nextPlayer.IsRevived())
                table.DealCards(nextPlayer);

            return table;
        }

        #region Player Working With Cards

        /// <summary>
        /// Deals 4 door cards and 4 treasure cards to a player.
        /// </summary>
        /// <param name="player">The player that will receive cards.</param>
        /// <returns>Return an updated table instance.</returns>
        public static Table DealCards(this Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            table = table with { DoorsCardDeck = table.DoorsCardDeck.TakeRange(4, out var doorCards) };
            table = table with { TreasureCardDeck = table.TreasureCardDeck.TakeRange(4, out var treasureCards) };
            player.ReceiveCards(doorCards.ToArray(), treasureCards.ToArray());
            return table;
        }

        /// <summary>
        /// TODO: Make sure the cards do not go into the discard pile as they should go 
        /// at the end of turn (unless a card has been player that says differently).
        /// </summary>
        /// <param name="player">The player whos avatar should be killed.</param>
        /// <returns>Return an updated table instance.</returns>
        public static Table KillPlayer(this Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var cardsToDiscard = player.Die();
            cardsToDiscard.ForEach(card => card.Discard(table));
            return table;
        }

        /// <summary>
        /// TODO: Make sure the cards do not go into the discard pile as they should go 
        /// at the end of turn (unless a card has been player that says differently).
        /// </summary>
        /// <param name="player">The player whos hand should be discarded.</param>
        /// <returns>Return an updated table instance.</returns>
        public static Table DiscardPlayersHand(this Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.YourHand.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            player.YourHand.OfType<TreasureCard>().ForEach(card => card.Discard(table));
            return table;
        }

        /// <summary>
        /// TODO: Make sure the cards do not go into the discard pile as they should go 
        /// at the end of turn (unless a card has been player that says differently).
        /// </summary>
        /// <param name="player">The player whos backpack should be discarded.</param>
        /// <returns>Return an updated table instance.</returns>
        public static Table DiscardPlayersBackpack(this Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Backpack.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            player.Backpack.OfType<TreasureCard>().ForEach(card => card.Discard(table));
            return table;
        }

        /// <summary>
        /// TODO: Make sure the cards do not go into the discard pile as they should go 
        /// at the end of turn (unless a card has been player that says differently).
        /// </summary>
        /// <param name="player">The player whos equipped items should be discarded.</param>
        /// <returns>Return an updated table instance.</returns>
        public static Table DiscardPlayersEquipped(this Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.Equipped.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            player.Equipped.OfType<TreasureCard>().ForEach(card => card.Discard(table));
            return table;
        }

        #endregion
    }
}
