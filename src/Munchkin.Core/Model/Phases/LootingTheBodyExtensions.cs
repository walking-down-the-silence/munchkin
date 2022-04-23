using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public static class LootingTheBodyExtensions
    {
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
