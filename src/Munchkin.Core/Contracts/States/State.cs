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

        public TAttribute GetProperty<TAttribute>() where TAttribute : Properties.Attribute
        {
            return _attributes.OfType<TAttribute>().FirstOrDefault();
        }

        public int AggregateProperties<TAttribute>(Func<TAttribute, int> selector)
        {
            return _attributes.OfType<TAttribute>().Aggregate(0, (total, next) => total + selector(next));
        }

        public void AddProperty<TAttribute>(TAttribute attribute) where TAttribute : Properties.Attribute
        {
            if (attribute is null) throw new ArgumentNullException(nameof(attribute));

            _attributes.Add(attribute);
        }
    }
}