using MediatR;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Contains all data about the game & it's context
    /// </summary>
    public class Table
    {
        private Table()
        {
            Dungeon = new Dungeon(this);
            Players = new CircularList<Player>();
            TreasureCardDeck = new CardDeck<TreasureCard>();
            DoorsCardDeck = new CardDeck<DoorsCard>();
            DiscardedTreasureCards = new CardDeck<TreasureCard>();
            DiscardedDoorsCards = new CardDeck<DoorsCard>();
        }

        public static Table Empty() => new();

        /// <summary>
        /// Gets the list of players
        /// </summary>
        public CircularList<Player> Players { get; private set; }

        /// <summary>
        /// Gets the treasures card deck
        /// </summary>
        public CardDeck<TreasureCard> TreasureCardDeck { get; private set; }

        /// <summary>
        /// Gets the doors card deck
        /// </summary>
        public CardDeck<DoorsCard> DoorsCardDeck { get; private set; }

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
        /// The winning level number
        /// </summary>
        public int WinningLevel { get; private set; }

        /// <summary>
        /// Request sink that is used for player interaction when a selection or decision is needed
        /// </summary>
        public IMediator RequestSink { get; }

        /// <summary>
        /// Gets if any of the players has won the game.
        /// </summary>
        public bool IsGameWon => Players.Any(x => x.Level >= WinningLevel);

        /// <summary>
        /// Sets the target level required to win the game.
        /// </summary>
        /// <param name="winningLevel">Target level.</param>
        public void SetWinningLevel(int winningLevel) => WinningLevel = winningLevel;

        /// <summary>
        /// Assigns the players to the gaming table.
        /// </summary>
        /// <param name="players">The collection of players to assign.</param>
        public Table WithPlayers(IReadOnlyCollection<Player> players)
        {
            Players = new CircularList<Player>(players);
            return this;
        }

        /// <summary>
        /// Appends the treasure cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cars to add.</param>
        public Table WithTreasureDeck(IReadOnlyCollection<TreasureCard> cards)
        {
            TreasureCardDeck = new CardDeck<TreasureCard>(cards);
            return this;
        }

        /// <summary>
        /// Appends the door cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cards to add.</param>
        public Table WithDoorDeck(IReadOnlyCollection<DoorsCard> cards)
        {
            DoorsCardDeck = new CardDeck<DoorsCard>(cards);
            return this;
        }
    }
}
