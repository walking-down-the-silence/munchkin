using Munchkin.Core.Contracts.Attributes;
using System.Collections.Generic;

namespace Munchkin.Core.Contracts.States
{
    /// <summary>
    /// Defines the state with a set of attributes/values.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Gets all the attributes of the state.
        /// </summary>
        IReadOnlyCollection<Attribute> Attributes { get; }

        /// <summary>
        /// Sets and additional attribute to the state.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="attribute">The attribute instance.</param>
        void AddProperty<TAttribute>(TAttribute attribute) where TAttribute : Attribute;

        /// <summary>
        /// Gets the specific attribute of the state.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        TAttribute GetProperty<TAttribute>() where TAttribute : Attribute;
    }
}
