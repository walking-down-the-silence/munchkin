using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Contracts.States
{
    public abstract class State : IState
    {
        private readonly List<Attributes.Attribute> _attributes = new();

        /// <summary>
        /// All the attributes that the state has.
        /// </summary>
        public IReadOnlyCollection<Attributes.Attribute> Attributes => _attributes.AsReadOnly();

        /// <summary>
        /// Gets a property of specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        public TAttribute GetProperty<TAttribute>() where TAttribute : Attributes.Attribute
        {
            return _attributes.OfType<TAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// Add a property of specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <param name="attribute"></param>
        public void AddProperty<TAttribute>(TAttribute attribute) where TAttribute : Attributes.Attribute
        {
            if (attribute is null) throw new ArgumentNullException(nameof(attribute));

            _attributes.Add(attribute);
        }

        /// <summary>
        /// Clears the state attributes.
        /// </summary>
        public virtual void Reset()
        {
            _attributes.Clear();
        }
    }
}