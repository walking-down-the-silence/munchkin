using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using System.Linq;

namespace Munchkin.Core.Services
{
    public static class PlayerAvatar
    {
        public static void Revive(Table table, Player player)
        {
            var doorCards = table.DoorsCardDeck.TakeRange(4);
            var treasureCards = table.TreasureCardDeck.TakeRange(4);
            player.Revive(doorCards.ToArray(), treasureCards.ToArray());
        }

        public static void Kill(Table table, Player player)
        {
            var cardsToDiscard = player.Kill();
            cardsToDiscard.ForEach(card => card.Discard(table));
        }

        public static void DiscardHand(Table table, Player player)
        {
            player.YourHand.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            player.YourHand.OfType<TreasureCard>().ForEach(card => card.Discard(table));
        }
    }
}
