using Munchkin.Core.Contracts.Cards;
using System;
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
    public record Charity(
        Table Table,
        Player Player,
        ImmutableArray<Card> TemporaryPile)
    {
        public static Charity From(Table table, Player currentPlayer)
        {
            return new Charity(table, currentPlayer, ImmutableArray<Card>.Empty);
        }

        public static Charity Reduce(Charity state, ICharityAction action)
        {
            return action switch
            {
                DiscardDoorAction discardDoor           => DiscardDoor(state, discardDoor.Card),
                DiscardTreasureAction discardTreasure   => DiscardTreasure(state, discardTreasure.Card),
                GiveAwayAction giveAway                 => GiveAway(state, giveAway.Card, giveAway.Player),
                _                                       => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        public static Charity DiscardTreasure(Charity state, TreasureCard card)
        {
            state.Player.Discard(card);
            state.Table.DiscardedTreasureCards.Put(card);

            var availableActions = ImmutableList.CreateRange(new[]
            {
                TurnActions.Player.DiscardDoor,
                TurnActions.Player.DiscardTreasure,
                TurnActions.Player.GiveAway
            });
            //return ActionResult.Create<Charity>(null, availableActions);
            return state;
        }

        public static Charity DiscardDoor(Charity state, DoorsCard card)
        {
            state.Player.Discard(card);
            state.Table.DiscardedDoorsCards.Put(card);

            var availableActions = ImmutableList.CreateRange(new[]
            {
                TurnActions.Player.DiscardDoor,
                TurnActions.Player.DiscardTreasure,
                TurnActions.Player.GiveAway
            });
            //return ActionResult.Create<Charity>(null, availableActions);
            return state;
        }

        public static Charity GiveAway(Charity state, TreasureCard card, Player taker)
        {
            state.Player.Discard(card);
            taker.PutInBackpack(card);

            var availableActions = ImmutableList.CreateRange(new[]
            {
                TurnActions.Player.DiscardDoor,
                TurnActions.Player.DiscardTreasure,
                TurnActions.Player.GiveAway
            });
            //return ActionResult.Create<Charity>(null, availableActions);
            return state;
        }
    }
}
