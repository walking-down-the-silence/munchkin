using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class TableExtensions
    {
        /// <summary>
        /// Gets if any of the players has won the game.
        /// </summary>
        public static bool IsGameOver(this Table table) => table.Players.Any(x => x.Level >= table.WinningLevel);

        /// <summary>
        /// Gets if the table is empty and has no players besides it.
        /// </summary>
        /// <param name="table">The table to chak against.</param>
        public static bool IsEmpty(this Table table) => !table.Players.Any();

        #region Player Working With Cards

        /// <summary>
        /// Deals 4 door cards and 4 treasure cards to a player.
        /// </summary>
        /// <param name="player">The player that will receive cards.</param>
        /// <returns>Return an updated table instance.</returns>
        public static Table DealCards(this Table table, Player player)
        {
            var doorCards = table.DoorsCardDeck.TakeRange(4);
            var treasureCards = table.TreasureCardDeck.TakeRange(4);
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
            player.Equipped.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            player.Equipped.OfType<TreasureCard>().ForEach(card => card.Discard(table));
            return table;
        }

        #endregion
    }
}
