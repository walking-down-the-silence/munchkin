using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a player avatar has died and the cards should be looted.
    /// </summary>
    public static class Death
    {
        /// <summary>
        /// Loots the dead players body by takes a card from him and giving to the one looting.
        /// </summary>
        /// <param name="table">The table where the game takes place.</param>
        /// <param name="giver">The player whos body is looted.</param>
        /// <param name="taker">The player who is taking the card from the body.</param>
        /// <param name="takenBy">A collection of players who had not yet looted the body.</param>
        /// <param name="card">The card that whould be taken from the body.</param>
        /// <returns>Returns and updated table instance.</returns>
        public static Table LootTheBody(Table table, Player giver, Player taker, IReadOnlyCollection<Player> takenBy, Card card)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(giver, nameof(giver));
            ArgumentNullException.ThrowIfNull(takenBy, nameof(takenBy));
            ArgumentNullException.ThrowIfNull(takenBy, nameof(takenBy));
            ArgumentNullException.ThrowIfNull(card, nameof(card));

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
