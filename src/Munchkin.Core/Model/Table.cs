using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Expansions;
using Munchkin.Extensions.Threading;
using Munchkin.Primitives;
using Munchkin.Primitives.Abstractions;
using Munchkin.Primitives.Immutable;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines a gaming table that holds the state of the game and everything that is going on in it.
    /// </summary>
    public sealed record Table
    {
        private readonly IShuffleAlgorithm<Card> _shuffleAlgorithm;
        private readonly List<object> _actionLog = new();
        private ImmutableList<object> _eventLog = ImmutableList<object>.Empty;
        private ImmutableDictionary<string, ExpansionOption> _availableExpansions;
        private ImmutableDictionary<string, ExpansionOption> _selectedExpansions;
        private ImmutableArray<Card> _doorCards;
        private ImmutableArray<Card> _treasureCards;

        private Table(IShuffleAlgorithm<Card> shuffleAlgorithm)
        {
            _shuffleAlgorithm = shuffleAlgorithm;
            _availableExpansions = ImmutableDictionary<string, ExpansionOption>.Empty;
            _selectedExpansions = ImmutableDictionary<string, ExpansionOption>.Empty;

            Players = new CircularList<Player>();
            TreasureCardDeck = ImmutableCardDeck.Create<TreasureCard>(shuffleAlgorithm);
            DoorsCardDeck = ImmutableCardDeck.Create<DoorsCard>(shuffleAlgorithm);
            DiscardedTreasureCards = ImmutableCardDeck.Create<TreasureCard>(shuffleAlgorithm);
            DiscardedDoorsCards = ImmutableCardDeck.Create<DoorsCard>(shuffleAlgorithm);
            DungeonCards = ImmutableArray<Card>.Empty;
        }

        /// <summary>
        /// Gets an empty table with default shuffle algorithm.
        /// </summary>
        /// <returns>Returns an instance of the table.</returns>
        public static Table Empty() => new(new DefaultShuffleAlgorithm<Card>());

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
        /// Gets the treasures card deck.
        /// </summary>
        public ImmutableCardDeck<TreasureCard> TreasureCardDeck { get; set; }

        /// <summary>
        /// Gets the doors card deck.
        /// </summary>
        public ImmutableCardDeck<DoorsCard> DoorsCardDeck { get; set; }

        /// <summary>
        /// Gets the discarded treasure cards.
        /// </summary>
        public ImmutableCardDeck<TreasureCard> DiscardedTreasureCards { get; set; }

        /// <summary>
        /// Gets the discarded door cards.
        /// </summary>
        public ImmutableCardDeck<DoorsCard> DiscardedDoorsCards { get; set; }

        /// <summary>
        /// The winning level number.
        /// </summary>
        public int WinningLevel { get; private set; }

        /// <summary>
        /// Gets if the game has started the table is closed for joining.
        /// </summary>
        public bool IsClosed { get; set; }

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
        public ImmutableArray<Card> DungeonCards { get; set; }

        /// <summary>
        /// Gets an ordered collection of events that happened to the table.
        /// </summary>
        public IReadOnlyCollection<object> EventLog => _eventLog;

        /// <summary>
        /// Gets the series of events that happened as the result of any action performed.
        /// </summary>
        public ICollection<object> ActionLog => _actionLog;

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
            return this with { WinningLevel = winningLevel };
        }

        /// <summary>
        /// Sets the requests sink instance used to communicate with outside world.
        /// </summary>
        /// <param name="requestSink">The request sink implementation instance.</param>
        public Table WithRequestSink(IMediator requestSink)
        {
            ArgumentNullException.ThrowIfNull(requestSink);
            
            return this with { RequestSink = requestSink };
        }

        /// <summary>
        /// Appends the treasure cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">Cars to add.</param>
        public Table WithTreasureDeck(IReadOnlyCollection<TreasureCard> cards)
        {
            ArgumentNullException.ThrowIfNull(cards);

            var table = this with
            {
                _treasureCards = ImmutableArray.CreateRange(cards.OfType<Card>()),
                TreasureCardDeck = ImmutableCardDeck.Create<TreasureCard>(_shuffleAlgorithm, cards)
            };

            return table;
        }

        /// <summary>
        /// Appends the door cards to the deck. Can be used for additional expansions.
        /// </summary>
        /// <param name="cards">The cards to add.</param>
        public Table WithDoorDeck(IReadOnlyCollection<DoorsCard> cards)
        {
            ArgumentNullException.ThrowIfNull(cards);

            var table = this with
            {
                _doorCards = ImmutableArray.CreateRange(cards.OfType<Card>()),
                DoorsCardDeck = ImmutableCardDeck.Create<DoorsCard>(_shuffleAlgorithm, cards)
            };

            return table;
        }

        /// <summary>
        /// Adds all available expansions to the table as options to include.
        /// </summary>
        /// <param name="expansions">The collection </param>
        /// <returns></returns>
        public Table WithExpansions(IReadOnlyCollection<ExpansionOption> expansions)
        {
            ArgumentNullException.ThrowIfNull(expansions);

            var table = this with
            {
                _selectedExpansions = ImmutableDictionary<string, ExpansionOption>.Empty,
                _availableExpansions = ImmutableDictionary.CreateRange(
                    expansions.Select(expansion => KeyValuePair.Create(expansion.Code, expansion)))
            };

            return table;
        }

        #endregion

        #region Working With Cards

        public Card FindCard(Func<Card, bool> filter)
        {
            return _doorCards.FirstOrDefault(filter)
                ?? _treasureCards.FirstOrDefault(filter);
        }

        public Table TakeDoor(out DoorsCard card)
        {
            return this with { DoorsCardDeck = DoorsCardDeck.Take(out card) };
        }

        public Table TakeTreasure(out TreasureCard card)
        {
            return this with { TreasureCardDeck = TreasureCardDeck.Take(out card) };
        }

        /// <summary>
        /// Play the card and put it into the temporary pile.
        /// </summary>
        /// <param name="card">The card to play.</param>
        /// <returns></returns>
        public Table Play(Card card)
        {
            ArgumentNullException.ThrowIfNull(card);

            card.Owner?.Discard(card);
            card.Play(this);

            return this with { DungeonCards = DungeonCards.Add(card) };
        }

        /// <summary>
        /// Puts the card into respective discard pile.
        /// </summary>
        /// <param name="card">The card to discard.</param>
        /// <returns></returns>
        public Table Discard(Card card)
        {
            ArgumentNullException.ThrowIfNull(card);

            card.Owner?.Discard(card);

            var table = this with { DungeonCards = DungeonCards.Remove(card) };

            table = card switch
            {
                TreasureCard treasure => table with { DiscardedTreasureCards = DiscardedTreasureCards.Put(treasure) },
                DoorsCard door => table with { DiscardedDoorsCards = DiscardedDoorsCards.Put(door) },
                _ => throw new ArgumentException("The card should either be of type Door or Treasure.", nameof(card))
            };

            return table;
        }

        #endregion

        #region Including Expansions

        /// <summary>
        /// Marks the expansion as included into the game, before the game actually begins.
        /// </summary>
        /// <param name="code">The code for the expansion.</param>
        /// <returns>Returns if the option was included.</returns>
        public (Table Table, SelectExpansionResult Result) IncludeExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return (this, SelectExpansionResult.InvalidOptionCode);

            var table = this with
            {
                _selectedExpansions = _selectedExpansions.SetItem(code, _availableExpansions[code]),
                _availableExpansions = _availableExpansions.Remove(code)
            };

            return (table, SelectExpansionResult.OptionSelected);
        }

        /// <summary>
        /// Marks the expansion as excluded from the game, before the game actually begins.
        /// </summary>
        /// <param name="code">The code for the expansion.</param>
        /// <returns>Returns if the option was excluded.</returns>
        public (Table Table, SelectExpansionResult Result) ExcludeExpansion(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return (this, SelectExpansionResult.InvalidOptionCode);

            var table = this with
            {
                _availableExpansions = _availableExpansions.SetItem(code, _selectedExpansions[code]),
                _selectedExpansions = _selectedExpansions.Remove(code)
            };

            return (table, SelectExpansionResult.OptionUnselected);
        }

        #endregion

        #region Joining The Table

        /// <summary>
        /// Add the player to the table/game.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>Returns if the player was able to join.</returns>
        public (Table Table, JoinTableResult Result) Join(Player player)
        {
            if (player is null)
                return (this, JoinTableResult.UserNotValid);

            if (IsClosed)
                return (this, JoinTableResult.Full);

            // TODO: Make sure the the player was not yet added.
            Players.Add(player);
            return (this, JoinTableResult.Joined);
        }

        /// <summary>
        /// Removed the player from the table/game.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>Returns if the player was able to leave.</returns>
        public (Table Table, JoinTableResult Result) Leave(Player player)
        {
            if (player is null)
                return (this, JoinTableResult.UserNotValid);

            if (IsClosed)
                return (this, JoinTableResult.Full);

            if (!Players.Any())
                return (this, JoinTableResult.Empty);

            // TODO: Remove the actual instance by finding it first.
            Players.Remove(player);
            return (this, JoinTableResult.Left);
        }

        #endregion
    }
}
