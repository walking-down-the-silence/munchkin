using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a player avatar has died and the cards needs to be salvaged.
    /// </summary>
    /// <param name="Table">The table where the game is played.</param>
    /// <param name="TakenFrom">The player whos avatar died.</param>
    /// <param name="TakenBy">A collection of players who whould take a card from another player.</param>
    /// <param name="TemporaryPile">The card collection that can be taken.</param>
    public record Death(
        Player TakenFrom,
        ImmutableArray<Player> TakenBy,
        ImmutableArray<Card> TemporaryPile)
    {
        public static Death From(Table table, Player player)
        {
            // NOTE: Starting with the player with the highest Level, everyone else chooses one
            // card... in case of ties in Level, roll a die.
            // Dead characters cannot receive cards for any reason, not even Charity, and
            // cannot level up or win the game.
            var otherPlayers = ImmutableArray.CreateRange(table.Players
                .Where(p => p != player)
                .Where(p => !p.IsDead)
                .OrderByDescending(p => p.Level));

            // NOTE: Looting The Body: Lay out your hand beside the cards you had in play
            // (making sure not to include the cards mentioned above). If you have an Item
            // carried by a Hireling or attached to a Cheat!card, separate those cards.
            var cardsToChooseFrom = ImmutableArray.CreateRange(player.AllCards());

            // TODO: Ensure that player avatar is dead when reaching this point
            return new Death(player, otherPlayers, cardsToChooseFrom);
        }

        public static Death Reduce(Death state, IDeathAction action)
        {
            return action switch
            {
                LootTheBody lootTheBody => LootTheBody(state, lootTheBody.Player, lootTheBody.Card),
                _                       => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        public static Death LootTheBody(Death state, Player taker, Card card)
        {
            // NOTE: Looted cards go into players’ hands.
            state.TakenFrom.Discard(card);
            taker.TakeInHand(card);

            // NOTE: Once everyone gets one card, discard the rest.
            //if (!TakenBy.Any())
            //{
            //    table.DiscardedDoorsCards.PutRange(TemporaryDiscard.OfType<DoorsCard>());
            //    table.DiscardedTreasureCards.PutRange(TemporaryDiscard.OfType<TreasureCard>());
            //}

            //return turn with
            //{
            //    TemporaryPile = turn.TemporaryPile.Remove(card),
            //    //TakenBy = turn.TakenBy.Add(taker),
            //};
            //return ActionResult.Create<Death>(null, null, null);
            return state;
        }
    }
}
