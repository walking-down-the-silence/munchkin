using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// If you have more than five cards in your hand, you must play
    /// enough cards to get you to five or below.If you cannot, or do not want to, you
    /// must give the excess cards to the player with the lowest Level.If players are
    /// tied for lowest, divide the cards as evenly as possible, but it’s up to you who
    /// gets the bigger set(s) of leftovers.
    /// </summary>
    /// <param name="Table">The table where the game takes place.</param>
    /// <param name="Player">The player that has more than 5 cards in hand.</param>
    /// <param name="ExcessCards">The excessive cards that a player has.</param>
    public record Charity(
        Table Table,
        Player Player,
        ImmutableList<Card> ExcessCards
    )
    : StateBase<Charity>(Table, ImmutableList<Attribute>.Empty);

    public static class CharityExtensions
    {
        public static IState From(Table table, Player player)
        {
            var excessCards = ImmutableList.CreateRange(player.YourHand);
            return new Charity(
                table,
                player,
                excessCards);
        }

        public static IState DiscardTreasure(this Charity state, TreasureCard card)
        {
            state.Table.Players.Current.Discard(card);
            state.Table.DiscardedTreasureCards.Put(card);
            return state;
        }

        public static IState DiscardDoor(this Charity state, DoorsCard card)
        {
            state.Table.Players.Current.Discard(card);
            state.Table.DiscardedDoorsCards.Put(card);
            return state;
        }

        public static IState Give(this Charity state, TreasureCard card, Player giver, Player taker)
        {
            giver.Discard(card);
            taker.PutInBackpack(card);
            return state;
        }

        public static IState EndTurn(this Charity state) =>
            TurnExtensions.From(state.Table);
    }
}
