using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public static class LootingTheBodyExtensions
    {
        public static LootingTheBody From(Table table)
        {
            var targetPlayer = table.Players.Current;

            // NOTE: Starting with the player with the highest Level, everyone else chooses one
            // card... in case of ties in Level, roll a die.
            // Dead characters cannot receive cards for any reason, not even Charity, and
            // cannot level up or win the game.
            var otherPlayers = ImmutableList.CreateRange(table.Players
                .Where(p => p != targetPlayer)
                .Where(p => !p.IsDead)
                .OrderByDescending(p => p.Level));

            // NOTE: Looting The Body: Lay out your hand beside the cards you had in play
            // (making sure not to include the cards mentioned above). If you have an Item
            // carried by a Hireling or attached to a Cheat!card, separate those cards.
            var cardsToBeTaken = ImmutableList.CreateRange(targetPlayer.AllCards());

            // TODO: Ensure that player avatar is dead when reaching this point
            return new LootingTheBody(
                targetPlayer,
                otherPlayers,
                cardsToBeTaken);
        }

        public static LootingTheBody TakeCard(this LootingTheBody death, Player taker, Card card)
        {
            // NOTE: Looted cards go into players’ hands.
            death.TakenFrom.Discard(card);
            taker.TakeInHand(card);

            return death with
            {
                TemporaryDiscard = death.TemporaryDiscard.Remove(card),
                TakenBy = death.TakenBy.Add(taker),
            };
        }

        public static LootingTheBody Resolve(this LootingTheBody lootingTheBody, Table table)
        {
            // NOTE: Once everyone gets one card, discard the rest.
            table.DiscardedDoorsCards.PutRange(lootingTheBody.TemporaryDiscard.OfType<DoorsCard>());
            table.DiscardedTreasureCards.PutRange(lootingTheBody.TemporaryDiscard.OfType<TreasureCard>());

            return lootingTheBody with
            {
                TemporaryDiscard = ImmutableList<Card>.Empty
            };
        }
    }
}
