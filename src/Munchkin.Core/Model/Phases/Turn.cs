using Munchkin.Core.Contracts.Actions;
using System.Collections.Immutable;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines the actions for the current player turn in the game.
    /// </summary>
    public record Turn(Player Player, ImmutableArray<IAction> AvailableActions);
}
