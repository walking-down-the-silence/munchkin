using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;

namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Defines the state with a set of attributes/values.
    /// </summary>
    public interface IState<TState>
    {
        /// <summary>
        /// Gets all the attributes of the state.
        /// </summary>
        ImmutableList<Attribute> Attributes { get; }

        /// <summary>
        /// Sets and additional attribute to the state.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="attribute">The attribute instance.</param>
        TState AddProperty<TAttribute>(TAttribute attribute) where TAttribute : Attribute;

        /// <summary>
        /// Gets the specific attribute of the state.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        TAttribute GetProperty<TAttribute>() where TAttribute : Attribute;
    }
}
