using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Expansions;
using Munchkin.Core.Stages;
using Munchkin.Extensions.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines a gaming table that hold the state of the game everything that is going on in the game.
    /// </summary>
    public sealed class Table
    {
        private readonly Dictionary<string, Player> _players = new();
        private readonly List<ExpansionOption> _expansionOptions = new();
        private readonly Dictionary<string, ExpansionOption> _selectedOptions = new();

        private Table()
        {
            Dungeon = Dungeon.KickOpenTheDoor(null);
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

        /// <summary>
        /// Dungeon state representing all goods in this dungeon.
        /// </summary>
        public Dungeon Dungeon { get; }

        /// <summary>
        /// Gets the ongoing trades bebtween players.
        /// </summary>
        public IReadOnlyCollection<Trade> OngoingTrades { get; }

        /// <summary>
        /// Request sink that is used for player interaction when a selection or decision is needed.
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
            RequestSink = requestSink
                ?? throw new ArgumentNullException(nameof(requestSink));
            return this.Unit();
        }

        /// <summary>
        /// Assigns the players to the gaming table.
        /// </summary>
        /// <param name="players">The collection of players to assign.</param>
        public Task<Table> WithPlayers(IReadOnlyCollection<Player> players)
        {
            Players = players is null
                ? throw new ArgumentNullException(nameof(players))
                : new CircularList<Player>(players);
            return this.Unit();
        }

        /// <summary>
        /// Appends the treasure cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cars to add.</param>
        public Task<Table> WithTreasureDeck(IReadOnlyCollection<TreasureCard> cards)
        {
            TreasureCardDeck = cards is null
                ? throw new ArgumentNullException(nameof(cards))
                : new CardDeck<TreasureCard>(cards);
            return this.Unit();
        }

        /// <summary>
        /// Appends the door cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cards to add.</param>
        public Task<Table> WithDoorDeck(IReadOnlyCollection<DoorsCard> cards)
        {
            DoorsCardDeck = cards is null
                ? throw new ArgumentNullException(nameof(cards))
                : new CardDeck<DoorsCard>(cards);
            return this.Unit();
        }

        public Task<Table> WithExpansions(IReadOnlyCollection<ExpansionOption> expansions)
        {
            if (expansions is null)
                return this.Unit();

            _expansionOptions.Clear();
            _expansionOptions.AddRange(expansions);
            return this.Unit();
        }

        public Task<IReadOnlyCollection<ExpansionSelection>> GetExpansionSelections()
        {
            IReadOnlyCollection<ExpansionSelection> expansionOptions = _expansionOptions
                .Select(x => new ExpansionSelection(x.Code, x.Title, _selectedOptions.ContainsKey(x.Code)))
                .ToArray();
            return Task.FromResult(expansionOptions);
        }

        public Task<SelectExpansionResult> SelectExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Task.FromResult(SelectExpansionResult.InvalidOptionCode);

            var expansion = _expansionOptions.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            _selectedOptions[code] = expansion;
            return Task.FromResult(SelectExpansionResult.OptionSelected);
        }

        public Task<SelectExpansionResult> UnselectExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Task.FromResult(SelectExpansionResult.InvalidOptionCode);

            var expansion = _expansionOptions.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            _selectedOptions.Remove(code);
            return Task.FromResult(SelectExpansionResult.OptionUnselected);
        }

        public Task<JoinTableResult> Join(Player player)
        {
            if (player is null)
                return Task.FromResult(JoinTableResult.InvalidUser);

            _players[player.Nickname] = player;
            return Task.FromResult(JoinTableResult.JoinedRoom);
        }

        public Task<JoinTableResult> Leave(Player player)
        {
            if (player is null)
                return Task.FromResult(JoinTableResult.InvalidUser);

            if (!_players.Any())
                return Task.FromResult(JoinTableResult.RoomEmpty);

            if (_players.Remove(player.Nickname))
                return Task.FromResult(JoinTableResult.LeftRoom);

            return Task.FromResult(JoinTableResult.InvalidUser);
        }
    }
}
