using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Model;
using System.Collections.Immutable;

namespace Munchkin.Core.Contracts
{
    public interface IState
    {
        /// <summary>
        /// Gets the table for the current game.
        /// </summary>
        Table Table { get; }

        /// <summary>
        /// Gets all the attributes of the state.
        /// </summary>
        ImmutableList<Attribute> Attributes { get; }
    }

    /// <summary>
    /// Defines the state with a set of attributes/values.
    /// </summary>
    public interface IState<TState>  : IState
        where TState : IState<TState>
    {
    }

    public record StateBase<TState>(Table Table, Player CurrentPlayer, ImmutableList<Attribute> Attributes) : 
        IState<TState>
        where TState : IState<TState>;
}
