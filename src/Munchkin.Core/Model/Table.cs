using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Extensions.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Contains all data about the game & it's context
    /// </summary>
    public class Table
    {
        private Table()
        {
            Dungeon = new Dungeon();
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
        public ICircularCollection<Player> Players { get; private set; }

        /// <summary>
        /// Gets the treasures card deck
        /// </summary>
        public ICardDeck<TreasureCard> TreasureCardDeck { get; private set; }

        /// <summary>
        /// Gets the doors card deck
        /// </summary>
        public ICardDeck<DoorsCard> DoorsCardDeck { get; private set; }

        /// <summary>
        /// Gets the discarded treasure cards
        /// </summary>
        public ICardDeck<TreasureCard> DiscardedTreasureCards { get; }

        /// <summary>
        /// Gets the discarded door cards
        /// </summary>
        public ICardDeck<DoorsCard> DiscardedDoorsCards { get; }

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
        public IMediator RequestSink { get; private set; }

        /// <summary>
        /// Sets the target level required to win the game.
        /// </summary>
        /// <param name="winningLevel">Target level.</param>
        public Task<Table> WithWinningLevel(int winningLevel)
        {
            WinningLevel = winningLevel;
            return this.Unit();
        }

        /// <summary>
        /// Sets the requests sink instance used to communicate with outside world.
        /// </summary>
        /// <param name="requestSink">The request sink implementation instance.</param>
        public Task<Table> WithRequestSink(IMediator requestSink)
        {
            RequestSink = requestSink;
            return this.Unit();
        }

        /// <summary>
        /// Assigns the players to the gaming table.
        /// </summary>
        /// <param name="players">The collection of players to assign.</param>
        public Task<Table> WithPlayers(IReadOnlyCollection<Player> players)
        {
            if (players is null)
            {
                throw new System.ArgumentNullException(nameof(players));
            }

            Players = new CircularList<Player>(players);
            return this.Unit();
        }

        /// <summary>
        /// Appends the treasure cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cars to add.</param>
        public Task<Table> WithTreasureDeck(IReadOnlyCollection<TreasureCard> cards)
        {
            if (cards is null)
            {
                throw new System.ArgumentNullException(nameof(cards));
            }

            TreasureCardDeck = new CardDeck<TreasureCard>(cards);
            return this.Unit();
        }

        /// <summary>
        /// Appends the door cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cards to add.</param>
        public Task<Table> WithDoorDeck(IReadOnlyCollection<DoorsCard> cards)
        {
            if (cards is null)
            {
                throw new System.ArgumentNullException(nameof(cards));
            }

            DoorsCardDeck = new CardDeck<DoorsCard>(cards);
            return this.Unit();
        }
    }
}
