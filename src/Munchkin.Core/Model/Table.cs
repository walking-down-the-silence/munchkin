using MediatR;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Cards;
using Munchkin.Expansions;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Contains all data about the game & it's context
    /// </summary>
    public class Table
    {
        private Table(
            IMediator mediator,
            CircularList<Player> players,
            IEnumerable<TreasureCard> treasureCards,
            IEnumerable<DoorsCard> doorsCards,
            int winningLevel)
        {
            RequestSink = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            Players = players ?? throw new System.ArgumentNullException(nameof(players));
            WinningLevel = winningLevel;
            Dungeon = new Dungeon(this);

            // TODO: initialize and shuffle the decks with all cards from factory
            TreasureCardDeck = new CardDeck<TreasureCard>(treasureCards);
            DoorsCardDeck = new CardDeck<DoorsCard>(doorsCards);
            DiscardedTreasureCards = new CardDeck<TreasureCard>();
            DiscardedDoorsCards = new CardDeck<DoorsCard>();
        }

        /// <summary>
        /// Begins the game
        /// </summary>
        /// <param name="winningLevel"> The winning level. </param>
        public static Table Setup(
            IMediator mediator,
            IEnumerable<Player> players,
            ITreasuresFactory treasuresFactory,
            IDoorsFactory doorsFactory,
            int winningLevel)
        {
            var playersList = new CircularList<Player>(players);
            var treasureCards = treasuresFactory.GetTreasureCards();
            var doorsCards = doorsFactory.GetDoorsCards();
            var table = new Table(mediator, playersList, treasureCards, doorsCards, winningLevel);

            // give all players initial cards
            table.Players.ForEach(player => player.Revive(table));
            return table;
        }

        /// <summary>
        /// Gets the list of players
        /// </summary>
        public CircularList<Player> Players { get; }

        /// <summary>
        /// Gets the treasures card deck
        /// </summary>
        public CardDeck<TreasureCard> TreasureCardDeck { get; }

        /// <summary>
        /// Gets the doors card deck
        /// </summary>
        public CardDeck<DoorsCard> DoorsCardDeck { get; }

        /// <summary>
        /// Gets the discarded treasure cards
        /// </summary>
        public CardDeck<TreasureCard> DiscardedTreasureCards { get; }

        /// <summary>
        /// Gets the discarded door cards
        /// </summary>
        public CardDeck<DoorsCard> DiscardedDoorsCards { get; }

        /// <summary>
        /// Dungeon state representing all goods in this dungeon
        /// </summary>
        public Dungeon Dungeon { get; }

        /// <summary>
        /// Request sink that is used for player interaction when a selection or decision is needed
        /// </summary>
        public IMediator RequestSink { get; }

        /// <summary>
        /// The winning level number
        /// </summary>
        public int WinningLevel { get; }

        /// <summary>
        /// Gets if any of the players has won the game.
        /// </summary>
        public bool IsGameWon => Players.Any(x => x.Level >= WinningLevel);
    }
}
