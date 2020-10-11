using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards
{
    /// <summary>
    /// The base card type that describes the behaviour.
    /// </summary>
    public abstract class Card
    {
        private readonly List<Card> _boundCards = new List<Card>();
        private readonly List<IConditionalEffect<Table>> _effects = new List<IConditionalEffect<Table>>();
        private readonly List<IAttribute> _attributes = new List<IAttribute>();
        private readonly List<IActionDefinition<Table>> _actions = new List<IActionDefinition<Table>>();

        protected Card()
        {
        }

        protected Card(string title)
        {
            Title = title ?? throw new System.ArgumentNullException(nameof(title));
        }

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
        /// Gets the traits of the card.
        /// </summary>
        public IReadOnlyCollection<IAttribute> Attributes => _attributes.AsReadOnly();

        /// <summary>
        /// Gets the dynamic actions that a card has.
        /// </summary>
        public IReadOnlyCollection<IActionDefinition<Table>> Actions => _actions.AsReadOnly();

        /// <inheritdoc />
        public override string ToString() => $"{GetType().Name}: {Title}";

        /// <summary>
        /// Binds the card to the current one to be played along with it.
        /// </summary>
        /// <param name="card">The card to bind onto the current one.</param>
        public void Bind(Card card)
        {
            _boundCards.Add(card);
            card.BoundTo = this;
        }

        /// <summary>
        /// Assigns the card to the player that took it.
        /// </summary>
        /// <param name="player">The player who took the card.</param>
        public void Take(Player player)
        {
            Owner = player;
        }

        /// <summary>
        /// Executes logic of the card being played.
        /// </summary>
        /// <param name="context"> Game context that contains everything in the game. </param>
        /// <returns> The task to be awaited if playing a card requires players interaction. </returns>
        public abstract Task Play(Table context);

        /// <summary>
        /// Executes the discard logic for the card.
        /// </summary>
        /// <param name="context"> Game context that contains everything in the game. </param>
        public abstract void Discard(Table context);

        /// <summary>
        /// Add the dynamic action to the card.
        /// </summary>
        /// <param name="dynamicAction"> The concrete dynamic action. </param>
        protected void AddAction(IActionDefinition<Table> dynamicAction) => _actions.Add(dynamicAction);

        /// <summary>
        /// Adds an effect to the card.
        /// </summary>
        /// <param name="effect"> The <see cref="IConditionalEffect{TState}"/> instance. </param>
        protected void AddEffect(IConditionalEffect<Table> effect) => _effects.Add(effect);

        /// <summary>
        /// Add the property to the card.
        /// </summary>
        /// <param name="property"> The concrete property instance. </param>
        protected void AddProperty(Attribute property) => _attributes.Add(property);

        /// <summary>
        /// Gets the concrete property of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"> The <see cref="T"/> type of property. </typeparam>
        /// <returns> The concrete property instance. </returns>
        protected T GetProperty<T>() where T : Attribute => _attributes.OfType<T>().FirstOrDefault();
    }
}
