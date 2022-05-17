using MediatR;
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Expansions;
using Munchkin.Extensions.Threading;
using Munchkin.Primitives;
using Munchkin.Primitives.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines a gaming table that hold the state of the game everything that is going on in the game.
    /// </summary>
    public sealed class Table
    {
        private readonly List<Card> _doorCards = new List<Card>();
        private readonly List<Card> _treasureCards = new List<Card>();
        private ImmutableDictionary<string, ExpansionOption> _availableExpansions = ImmutableDictionary<string, ExpansionOption>.Empty;
        private ImmutableDictionary<string, ExpansionOption> _selectedExpansions = ImmutableDictionary<string, ExpansionOption>.Empty;
        private readonly IShuffleAlgorithm<Card> _shuffleAlgorithm;

        private Table(IShuffleAlgorithm<Card> shuffleAlgorithm)
        {
            Players = new CircularList<Player>();
            Turns = new CircularList<Turn>();
            TreasureCardDeck = new CardDeck<TreasureCard>(shuffleAlgorithm);
            DoorsCardDeck = new CardDeck<DoorsCard>(shuffleAlgorithm);
            DiscardedTreasureCards = new CardDeck<TreasureCard>(shuffleAlgorithm);
            DiscardedDoorsCards = new CardDeck<DoorsCard>(shuffleAlgorithm);
            _shuffleAlgorithm = shuffleAlgorithm;
            TemporaryPile = ImmutableArray<Card>.Empty;
            ActionLog = ImmutableStack<object>.Empty;
        }

        /// <summary>
        /// Gets an empty table with default shuffle algorithm.
        /// </summary>
        /// <returns>Returns an instance of the table.</returns>
        public static Table Empty() => new(default);

        /// <summary>
        /// Gets an empty table with custom shuffle algorithm.
        /// </summary>
        /// <param name="shuffleAlgorithm">The suffle algorithm implementation.</param>
        /// <returns>Returns an instance of the table.</returns>
        public static Table Empty(IShuffleAlgorithm<Card> shuffleAlgorithm) => new(shuffleAlgorithm);

        #region Properties

        /// <summary>
        /// Gets the list of players.
        /// </summary>
        public ICircularCollection<Player> Players { get; }

        /// <summary>
        /// Gets a collection of each players turn and actions available during that turn.
        /// </summary>
        public ICircularCollection<Turn> Turns { get; }

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
        /// Gets if the game has started the table is closed for joining.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Gets the expansions available for selection.
        /// </summary>
        public IReadOnlyCollection<ExpansionOption> AvailableExpansions =>
            _availableExpansions.Values.ToList();

        /// <summary>
        /// Gets the expansions that were selected to play.
        /// </summary>
        public IReadOnlyCollection<ExpansionOption> IncludedExpansions =>
            _selectedExpansions.Values.ToList();

        /// <summary>
        /// Gets the pile of cards played in the current turn.
        /// </summary>
        public ImmutableArray<Card> TemporaryPile { get; private set; }

        /// <summary>
        /// Gets the series of events that happened as the result of any action performed.
        /// </summary>
        public ImmutableStack<object> ActionLog { get; private set; }

        /// <summary>
        /// Request sink that is used for player interaction when a selection or decision is needed.
        /// </summary>
        public IMediator RequestSink { get; private set; }

        #endregion

        #region Setting The Preferences

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
        /// Appends the treasure cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cars to add.</param>
        public Table WithTreasureDeck(IReadOnlyCollection<TreasureCard> cards)
        {
            _treasureCards.Clear();
            _treasureCards.AddRange(cards);

            TreasureCardDeck = cards is null
                ? throw new ArgumentNullException(nameof(cards))
                : new CardDeck<TreasureCard>(_shuffleAlgorithm, cards);
            return this;
        }

        /// <summary>
        /// Appends the door cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">The cards to add.</param>
        public Table WithDoorDeck(IReadOnlyCollection<DoorsCard> cards)
        {
            _doorCards.Clear();
            _doorCards.AddRange(cards);

            DoorsCardDeck = cards is null
                ? throw new ArgumentNullException(nameof(cards))
                : new CardDeck<DoorsCard>(_shuffleAlgorithm, cards);
            return this;
        }

        /// <summary>
        /// Adds all available expansions to the table as options to include.
        /// </summary>
        /// <param name="expansions">The collection </param>
        /// <returns></returns>
        public Table WithExpansions(IReadOnlyCollection<ExpansionOption> expansions)
        {
            if (expansions is null)
                return this;

            _selectedExpansions = ImmutableDictionary<string, ExpansionOption>.Empty;
            _availableExpansions = ImmutableDictionary.CreateRange(
                expansions.Select(expansion => KeyValuePair.Create(expansion.Code, expansion)));

            return this;
        }

        #endregion

        /// <summary>
        /// Closes the table for joining/leaving and shuffles the decks.
        /// </summary>
        /// <returns></returns>
        public Table Setup()
        {
            IsClosed = true;

            // NOTE: Divide the cards into the Door deck and the Treasure deck. Shuffle both decks.
            DoorsCardDeck.Shuffle();
            TreasureCardDeck.Shuffle();

            // NOTE: Deal four cards from each deck to each player.
            Players.ForEach(player => this.DealCards(player));
            return this;
        }

        /// <summary>
        /// Defines an action that moves the turn to the next player.
        /// </summary>
        /// <returns>Returns an updated isntance of the table after the turn has moved to another player.</returns>
        public Table NextTurn()
        {
            var table = this;

            // NOTE: Put all the cards player during this turn into the respective dicard piles.
            table.DiscardedDoorsCards.PutRange(table.TemporaryPile.OfType<DoorsCard>());
            table.DiscardedTreasureCards.PutRange(table.TemporaryPile.OfType<TreasureCard>());

            // NOTE: When the next player begins his turn, your new character appears and can
            // help others in combat with his Level and Class or Race abilities... but you
            // have no cards, unless you receive Charity or gifts from other players.
            if (table.Players.Current.IsDead())
                table.Players.Current.Revive();

            var nextPlayer = table.Players.Next();
            table.Turns.Next();

            // NOTE: On your next turn, start by drawing four face-down cards from each deck
            // and playing any legal cards you want to, just as when you started the game.
            if (nextPlayer.IsRevived())
                table.DealCards(nextPlayer);

            return this;
        }

        public Card FindCard(Func<Card, bool> filter)
        {
            return _doorCards.FirstOrDefault(filter)
                ?? _treasureCards.FirstOrDefault(filter);
        }

        /// <summary>
        /// Play the card and put it into the temporary pile.
        /// </summary>
        /// <param name="card">The card to play.</param>
        /// <returns></returns>
        public Table Play(Card card)
        {
            card.Owner.Discard(card);
            card.Play(this);
            TemporaryPile = TemporaryPile.Add(card);
            return this;
        }

        /// <summary>
        /// Puts the card into respective discard pile.
        /// </summary>
        /// <param name="card">The card to discard.</param>
        /// <returns></returns>
        public Table Discard(Card card)
        {
            card.Owner.Discard(card);

            if (card is DoorsCard door)
                DiscardedDoorsCards.Put(door);

            if (card is TreasureCard treasure)
                DiscardedTreasureCards.Put(treasure);

            return this;
        }

        #region Including Expansions

        /// <summary>
        /// Marks the expansion as included into the game, before the game actually begins.
        /// </summary>
        /// <param name="code">The code for the expansion.</param>
        /// <returns>Returns if the option was included.</returns>
        public SelectExpansionResult IncludeExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return SelectExpansionResult.InvalidOptionCode;

            _selectedExpansions = _selectedExpansions.SetItem(code, _availableExpansions[code]);
            _availableExpansions = _availableExpansions.Remove(code);
            return SelectExpansionResult.OptionSelected;
        }

        /// <summary>
        /// Marks the expansion as excluded from the game, before the game actually begins.
        /// </summary>
        /// <param name="code">The code for the expansion.</param>
        /// <returns>Returns if the option was excluded.</returns>
        public SelectExpansionResult ExcludeExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return SelectExpansionResult.InvalidOptionCode;

            _availableExpansions = _availableExpansions.SetItem(code, _selectedExpansions[code]);
            _selectedExpansions = _selectedExpansions.Remove(code);
            return SelectExpansionResult.OptionUnselected;
        }

        #endregion

        #region Player Joining The Table

        /// <summary>
        /// Add the player to the table/game.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>Returns if the player was able to join.</returns>
        public JoinTableResult Join(Player player)
        {
            if (player is null)
                return JoinTableResult.UserNotValid;

            if (IsClosed)
                return JoinTableResult.Full;

            // TODO: Make sure the the player was not yet added.
            Players.Add(player);
            Turns.Add(new Turn(player, ImmutableArray<IAction>.Empty));
            return JoinTableResult.Joined;
        }

        /// <summary>
        /// Removed the player from the table/game.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>Returns if the player was able to leave.</returns>
        public JoinTableResult Leave(Player player)
        {
            if (player is null)
                return JoinTableResult.UserNotValid;

            if (IsClosed)
                return JoinTableResult.Full;

            if (!Players.Any())
                return JoinTableResult.Empty;

            // TODO: Remove the actual instance by finding it first.
            Players.Remove(player);
            Turns.Remove(new Turn(player, ImmutableArray<IAction>.Empty));
            return JoinTableResult.Left;
        }

        #endregion
    }
}
