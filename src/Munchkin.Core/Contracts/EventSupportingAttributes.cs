using System;
using System.Collections.Generic;

namespace Munchkin.Core.Contracts
{
    public abstract class EventSupportingAttributes : IEvent, ISupportAttributes
    {
        private readonly List<IAttribute> _attributes = new();

        public EventSupportingAttributes()
        {

        }

        /// <summary>
        /// Gets the date and time when the event was created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; }

        /// <summary>
        /// Gets the traits of the card.
        /// </summary>
        public IReadOnlyCollection<IAttribute> Attributes => _attributes.AsReadOnly();

        /// <summary>
        /// Add the property to the card.
        /// </summary>
        /// <param name="property"> The concrete property instance. </param>
        protected void AddAttribute(Attribute property) => _attributes.Add(property);
    }
}
