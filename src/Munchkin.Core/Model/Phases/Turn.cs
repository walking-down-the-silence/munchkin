using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Phases;
using System;
using System.Collections.Immutable;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines the state for the current player turn in the game.
    /// </summary>
    /// <param name="Table">The table where the game is played at.</param>
    /// <param name="CurrentPlayer">The current player in the game.</param>
    /// <param name="HelpingPlayer">The player who is helping current player in combat.</param>
    /// <param name="TemporaryPile">A pile of cards played by players during the turn.</param>
    public record Turn(
        Table Table,
        Player CurrentPlayer,
        Player HelpingPlayer,
        Dungeon Dungeon,
        ImmutableArray<Card> TemporaryPile)
    {
        /// <summary>
        /// Creates the first turn in the game.
        /// </summary>
        /// <param name="table">The table where the game is played at.</param>
        /// <returns>Returns an isntance of the current player turn.</returns>
        /// <exception cref="ArgumentNullException">Throws if the table parameter is null.</exception>
        public static Turn From(Table table)
        {
            if (table is null)
                throw new ArgumentNullException(nameof(table));

            // NOTE: Divide the cards into the Door deck and the Treasure deck. Shuffle both decks.
            table.DoorsCardDeck.Shuffle();
            table.TreasureCardDeck.Shuffle();

            // NOTE: Deal four cards from each deck to each player.
            table.Players.ForEach(player => table.ReceiveCards(player));

            return new Turn(table, null, table.Players.Current, null, ImmutableArray<Card>.Empty);
        }

        public static Turn Reduce(Turn state, IAction action)
        {
            return action switch
            {
                NextTurnAction _        => Next(state),
                PlayCardAction playCard => Play(state, playCard.Card),
                IDungeonAction dungeon  => state with { Dungeon = Dungeon.Reduce(state.Dungeon, dungeon) },
                _                       => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        /// <summary>
        /// Creates the next turn in the game based on the current table state.
        /// </summary>
        /// <param name="Turn">The current turn state.</param>
        /// <returns>Returns an isntance of the current player turn.</returns>
        public static Turn Next(Turn currentTurn)
        {
            var table = currentTurn.Table;

            // NOTE: Put all the cards player during this turn into the respective dicard piles.
            table.DiscardedDoorsCards.PutRange(currentTurn.TemporaryPile.OfType<DoorsCard>());
            table.DiscardedTreasureCards.PutRange(currentTurn.TemporaryPile.OfType<TreasureCard>());

            // NOTE: On your next turn, start by drawing four face-down cards from each deck
            // and playing any legal cards you want to, just as when you started the game.
            if (table.Players.Current.IsRevived)
                table.ReceiveCards(table.Players.Current);

            // NOTE: When the next player begins his turn, your new character appears and can
            // help others in combat with his Level and Class or Race abilities... but you
            // have no cards, unless you receive Charity or gifts from other players.
            if (table.Players.Current.IsDead)
                table.Players.Current.Revive();

            table.Players.Next();

            var availableActions = ImmutableList.Create(TurnActions.Dungeon.KickOpenTheDoor);
            var dungeon = Dungeon.From(table, table.Players.Current);
            var nextTurn = new Turn(table, table.Players.Current, null, dungeon, ImmutableArray<Card>.Empty);

            //return ActionResult.Create(turn, availableActions);
            return nextTurn;
        }

        public static Turn Play(Turn state, Card card)
        {
            return state with { TemporaryPile = state.TemporaryPile.Add(card) };
        }
    }
}
