using Munchkin.Core.Contracts.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    public abstract class State : IState
    {
        private readonly List<Properties.Attribute> _attributes = new List<Properties.Attribute>();

        /// <summary>
        /// All the attributes that the state has.
        /// </summary>
        public IReadOnlyCollection<Properties.Attribute> Attributes => _attributes.AsReadOnly();

        /// <summary>
        /// Gets a property of specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        public TAttribute GetProperty<TAttribute>() where TAttribute : Properties.Attribute
        {
            return _attributes.OfType<TAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// Add a property of specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <param name="attribute"></param>
        public void AddProperty<TAttribute>(TAttribute attribute) where TAttribute : Properties.Attribute
        {
            if (attribute is null) throw new ArgumentNullException(nameof(attribute));

            _attributes.Add(attribute);
        }
    }
}