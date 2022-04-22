using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Expansions;
using Munchkin.Extensions.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines a gaming table that hold the state of the game everything that is going on in the game.
    /// </summary>
    public sealed class Table
    {
        private readonly Dictionary<string, Player> _players = new();
        private readonly Dictionary<string, ExpansionSelection> _availableExpansions = new();
        private readonly Dictionary<string, ExpansionSelection> _selectedExpansions = new();

        private Table()
        {
            Players = new CircularList<Player>();
            TreasureCardDeck = new CardDeck<TreasureCard>();
            DoorsCardDeck = new CardDeck<DoorsCard>();
            DiscardedTreasureCards = new CardDeck<TreasureCard>();
            DiscardedDoorsCards = new CardDeck<DoorsCard>();
        }

        public static Table Empty() => new();

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        public ICircularCollection<Player> Players { get; private set; }

        /// <summary>
        /// Gets the treasures card deck.
        /// </summary>
        public ICardDeck<TreasureCard> TreasureCardDeck { get; private set; }

        /// <summary>
        /// Gets the doors card deck.
        /// </summary>
        public ICardDeck<DoorsCard> DoorsCardDeck { get; private set; }

        /// <summary>
        /// Gets the discarded treasure cards.
        /// </summary>
        public ICardDeck<TreasureCard> DiscardedTreasureCards { get; }

        /// <summary>
        /// Gets the discarded door cards.
        /// </summary>
        public ICardDeck<DoorsCard> DiscardedDoorsCards { get; }

        /// <summary>
        /// The winning level number.
        /// </summary>
        public int WinningLevel { get; private set; }

        public IReadOnlyCollection<ExpansionSelection> AvailableExpansions =>
            _availableExpansions.Values;

        public IReadOnlyCollection<ExpansionSelection> IncludedExpansions =>
            _selectedExpansions.Values;

        /// <summary>
        /// Request sink that is used for player interaction when a selection or decision is needed.
        /// </summary>
        public IMediator RequestSink { get; private set; }

        /// <summary>
        /// Sets the target level required to win the game.
        /// </summary>
        /// <param name="winningLevel">Target level.</param>
        public Table WithWinningLevel(int winningLevel)
        {
            WinningLevel = winningLevel;
            return this;
        }

        /// <summary>
        /// Sets the requests sink instance used to communicate with outside world.
        /// </summary>
        /// <param name="requestSink">The request sink implementation instance.</param>
        public Table WithRequestSink(IMediator requestSink)
        {
            RequestSink = requestSink
                ?? throw new ArgumentNullException(nameof(requestSink));
            return this;
        }

        /// <summary>
        /// Assigns the players to the gaming table.
        /// </summary>
        /// <param name="players">The collection of players to assign.</param>
        public Table WithPlayers(IReadOnlyCollection<Player> players)
        {
            Players = players is null
                ? throw new ArgumentNullException(nameof(players))
                : new CircularList<Player>(players);
            return this;
        }

        /// <summary>
        /// Appends the treasure cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cars to add.</param>
        public Table WithTreasureDeck(IReadOnlyCollection<TreasureCard> cards)
        {
            TreasureCardDeck = cards is null
                ? throw new ArgumentNullException(nameof(cards))
                : new CardDeck<TreasureCard>(cards);
            return this;
        }

        /// <summary>
        /// Appends the door cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cards to add.</param>
        public Table WithDoorDeck(IReadOnlyCollection<DoorsCard> cards)
        {
            DoorsCardDeck = cards is null
                ? throw new ArgumentNullException(nameof(cards))
                : new CardDeck<DoorsCard>(cards);
            return this;
        }

        public Table WithExpansions(IReadOnlyCollection<ExpansionOption> expansions)
        {
            if (expansions is null)
                return this;

            _availableExpansions.Clear();
            _selectedExpansions.Clear();

            _ = expansions
                .Select(x => _availableExpansions[x.Code] = ToExpansionSelection(x))
                .ToList();

            return this;
        }

        public SelectExpansionResult IncludeExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return SelectExpansionResult.InvalidOptionCode;

            _selectedExpansions[code] = _availableExpansions[code];
            _availableExpansions.Remove(code);
            return SelectExpansionResult.OptionSelected;
        }

        public SelectExpansionResult ExcludeExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return SelectExpansionResult.InvalidOptionCode;

            _availableExpansions[code] = _selectedExpansions[code];
            _selectedExpansions.Remove(code);
            return SelectExpansionResult.OptionUnselected;
        }

        public JoinTableResult Join(Player player)
        {
            if (player is null)
                return JoinTableResult.InvalidUser;

            _players[player.Nickname] = player;
            return JoinTableResult.JoinedRoom;
        }

        public JoinTableResult Leave(Player player)
        {
            if (player is null)
                return JoinTableResult.InvalidUser;

            if (!_players.Any())
                return JoinTableResult.RoomEmpty;

            if (_players.Remove(player.Nickname))
                return JoinTableResult.LeftRoom;

            return JoinTableResult.InvalidUser;
        }

        private static ExpansionSelection ToExpansionSelection(ExpansionOption x) =>
            new ExpansionSelection(x.Code, x.Title, false);
    }
}
