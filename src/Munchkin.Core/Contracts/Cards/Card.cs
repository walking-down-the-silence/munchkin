using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    /// <summary>
    /// The base card type that describes the behaviour.
    /// </summary>
    public abstract class Card :
        IEquatable<Card>,
        IDiscardable,
        IPlayable,
        ISupportAttributes
    {
        private readonly List<Card> _boundCards = new();
        private readonly List<IConditionalEffect<Table>> _effects = new();
        private readonly List<IAttribute> _attributes = new();
        private readonly List<IRule<Table>> _restrictions = new();

        protected Card(string code, string title)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Code { get; }

        /// <summary>
        /// Gets or sets the title for the card.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets or sets the player owning the card.
        /// </summary>
        public Player Owner { get; protected set; }

        /// <summary>
        /// Gets the card that current one is bound to play with.
        /// </summary>
        public Card BoundTo { get; protected set; }

        /// <summary>
        /// Gets the bound cards to current one.
        /// </summary>
        public IReadOnlyCollection<Card> BoundCards => _boundCards.AsReadOnly();

        /// <summary>
        /// Gets the list of effects of the card.
        /// </summary>
        public IReadOnlyCollection<IConditionalEffect<Table>> Effects => _effects.AsReadOnly();

        /// <summary>
        /// Gets the list of restriction rules where and when the cad can be played.
        /// </summary>
        public IReadOnlyCollection<IRule<Table>> Restrictions => _restrictions.AsReadOnly();

        /// <summary>
        /// Gets the traits of the card.
        /// </summary>
        public IReadOnlyCollection<IAttribute> Attributes => _attributes.AsReadOnly();

        /// <inheritdoc />
        public override string ToString() => $"{Title}, owned by: {(Owner is null ? "Nobody" : Owner.Nickname)}";

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as Card);

        /// <inheritdoc />
        public bool Equals(Card other) => string.Equals(Code, other?.Code, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Binds the card to the current one to be played along with it.
        /// </summary>
        /// <param name="card">The card to bind onto the current one.</param>
        public void Bind(Card card)
        {
            ArgumentNullException.ThrowIfNull(card, nameof(card));

            _boundCards.Add(card);
            card.BoundTo = this;
        }

        /// <summary>
        /// Assigns the card to the player that took it.
        /// </summary>
        /// <param name="player">The player who took the card.</param>
        public void TakenBy(Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            Owner = player;
        }

        /// <summary>
        /// Executes logic of the card being played.
        /// </summary>
        /// <param name="table"> Game table that contains everything in the game. </param>
        /// <returns> The task to be awaited if playing a card requires players interaction. </returns>
        public virtual Task Play(Table table) => Task.CompletedTask;

        /// <summary>
        /// Executes the discard logic for the card.
        /// </summary>
        /// <param name="table"> Game table that contains everything in the game. </param>
        public virtual void Discard(Table table)
        {
            Owner?.Discard(this);
            Owner = null;
            BoundTo = null;

            _boundCards.ForEach(card => card.Discard(table));
            _boundCards.Clear();
        }

        /// <summary>
        /// Adds an effect to the card.
        /// </summary>
        /// <param name="effect"> The <see cref="IConditionalEffect{TState}"/> instance. </param>
        protected void AddEffect(IConditionalEffect<Table> effect) => _effects.Add(effect);

        /// <summary>
        /// Adds the restriction to the card.
        /// </summary>
        /// <param name="restiction"></param>
        protected void AddRestriction(IRule<Table> restiction) => _restrictions.Add(restiction);

        /// <summary>
        /// Add the property to the card.
        /// </summary>
        /// <param name="property"> The concrete property instance. </param>
        protected void AddAttribute(IAttribute property) => _attributes.Add(property);

        /// <summary>
        /// Gets the concrete property of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"> The <see cref="T"/> type of property. </typeparam>
        /// <returns> The concrete property instance. </returns>
        protected T GetAttribute<T>() where T : IAttribute => _attributes.OfType<T>().FirstOrDefault();
    }
}
