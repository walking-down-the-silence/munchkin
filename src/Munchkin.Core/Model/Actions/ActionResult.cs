using System.Collections.Immutable;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines a set of data as the result of an action by a player.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned after doing an action.</typeparam>
    /// <param name="Result">The actual result of the action.</param>
    /// <param name="AvailableActions">A collection of action availabe to a player based on current state.</param>
    public record ActionResult<TResult>(TResult Result, ImmutableList<string> AvailableActions);

    public static class ActionResult
    {
        public static ActionResult<TResult> Create<TResult>(TResult Result, ImmutableList<string> AvailableActions) =>
            new(Result, AvailableActions);
    }
}
