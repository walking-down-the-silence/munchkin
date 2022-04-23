using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state of the dungeon that the player has entered.
    /// </summary>
    /// <param name="PlayedCards">All the played cards in the dungeon.</param>
    /// <param name="Attributes">All the attributes that the state has.</param>
    public record Dungeon(
        Table Table,
        Player CurrentPlayer
    )
    : StateBase<Dungeon>(Table, CurrentPlayer, ImmutableList<Attribute>.Empty);
}