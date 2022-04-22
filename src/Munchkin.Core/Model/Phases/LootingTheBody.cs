using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state when a player avatar has died and the cards needs to be salvaged.
    /// </summary>
    /// <param name="TakenFrom">The player whos avatar died.</param>
    /// <param name="TemporaryDiscard">The card collection that needs to be salvaged.</param>
    public record LootingTheBody(
        Player TakenFrom,
        ImmutableList<Player> TakenBy,
        ImmutableList<Card> TemporaryDiscard);
}
