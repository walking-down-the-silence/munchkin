using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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

    public record StateBase<TState>(
        Table Table,
        Player CurrentPlayer,
        ImmutableList<Attribute> Attributes
    )
    : IState<TState>
        where TState : IState<TState>;

    public static class StateExtensions
    {
    }
}
