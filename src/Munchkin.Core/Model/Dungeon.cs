using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    public class Dungeon : IState
    {
        private readonly List<Card> _playedCards = new();
        private readonly List<Contracts.Attributes.Attribute> _attributes = new();

        /// <summary>
        /// All the attributes that the state has.
        /// </summary>
        public IReadOnlyCollection<Contracts.Attributes.Attribute> Attributes => _attributes.AsReadOnly();

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        /// <summary>
        /// Gets a property of specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <returns>The attribute instance.</returns>
        public TAttribute GetProperty<TAttribute>() where TAttribute : Contracts.Attributes.Attribute
        {
            return _attributes.OfType<TAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// Add a property of specific type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute.</typeparam>
        /// <param name="attribute"></param>
        public void AddProperty<TAttribute>(TAttribute attribute) where TAttribute : Contracts.Attributes.Attribute
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
            _playedCards.Clear();
        }

        public void AddPlayedCard(Card card) => _playedCards.Add(card);

        public void RemovePlayedCard(Card card) => _playedCards.Remove(card);
    }
}