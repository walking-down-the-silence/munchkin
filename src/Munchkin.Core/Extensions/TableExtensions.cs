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

        public static void RevivePlayer(this Table table, Player player)
        {
            var doorCards = table.DoorsCardDeck.TakeRange(4);
            var treasureCards = table.TreasureCardDeck.TakeRange(4);
            player.Revive(doorCards.ToArray(), treasureCards.ToArray());
        }

        public static void KillPlayer(this Table table, Player player)
        {
            var cardsToDiscard = player.Kill();
            cardsToDiscard.ForEach(card => card.Discard(table));
        }

        public static void DiscardPlayersHand(this Table table, Player player)
        {
            player.YourHand.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            player.YourHand.OfType<TreasureCard>().ForEach(card => card.Discard(table));
        }
    }
}
