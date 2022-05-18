using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a player avatar has died and the cards needs to be salvaged.
    /// </summary>
    public static class Death
    {
        public static Table LootTheBody(Table table, Player giver, Player taker, IReadOnlyCollection<Player> takenBy, Card card)
        {
            // NOTE: Looted cards go into players’ hands.
            giver.Discard(card);
            taker.TakeInHand(card);

            // NOTE: Once everyone gets one card, discard the rest.
            if (!takenBy.Any())
            {
                // NOTE: Move all remaining cards go to discard except 'safe' ones.
                table.DungeonCards.AddRange(giver.AllLootableCards().OfType<DoorsCard>());
                table.DungeonCards.AddRange(giver.AllLootableCards().OfType<TreasureCard>());
            }

            return table;
        }
    }
}
