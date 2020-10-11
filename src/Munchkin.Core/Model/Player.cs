using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// The type that describes players behaviour
    /// </summary>
    public class Player
    {
        private readonly List<Card> _yourHand = new List<Card>();
        private readonly List<Card> _backpack = new List<Card>();
        private readonly List<Card> _equipped = new List<Card>();
        private readonly List<IActionDefinition<Table>> _actions;
        private bool _isDead;
        private int _level;

        public Player(string name, EGender gender)
        {
            Name = name;
            Gender = gender;
            _actions = new List<IActionDefinition<Table>>
            {
                new ActionDefinition<Table>("As For Help", () => new AskForHelpAction()),
                new ActionDefinition<Table>("Discard A Card", () => new DiscardCardAction()),
                new ActionDefinition<Table>("End Combat", () => new EndBattleAction()),
                new ActionDefinition<Table>("End Turn", () => new EndTurnAction()),
                new ActionDefinition<Table>("Kick Down The Door", () => new KickDownTheDoorAction()),
                new ActionDefinition<Table>("Look For Trouble", () => new LookForTroubleAction()),
                new ActionDefinition<Table>("Loot The Room", () => new LootTheRoomAction()),
                new ActionDefinition<Table>("Run Away", () => new RunAwayAction()),
                new ActionDefinition<Table>("Take Bad Stuff", () => new TakeBadStuffAction())
            };
        }

        /// <summary>
        /// Gets the players name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the players gender
        /// </summary>
        public EGender Gender { get; }

        /// <summary>
        /// Gets the players current level
        /// </summary>
        public int Level => _level;

        /// <summary>
        /// Gets if players hero is dead
        /// </summary>
        public bool IsDead => _isDead;

        /// <summary>
        /// Gets the closed out-of-game cards
        /// </summary>
        public IReadOnlyCollection<Card> YourHand => _yourHand;

        /// <summary>
        /// Gets that are in play and are equipped
        /// </summary>
        public IReadOnlyCollection<Card> Equipped => _equipped;

        /// <summary>
        /// Gets that are in play but are not equipped
        /// </summary>
        public IReadOnlyCollection<Card> Backpack => _backpack;

        /// <summary>
        /// Gets the dynamic actions for the user
        /// </summary>
        public IReadOnlyCollection<IActionDefinition<Table>> Actions => _actions
            .Concat(_equipped.SelectMany(card => card.Actions))
            .ToList()
            .AsReadOnly();

        /// <summary>
        /// Levels up the player
        /// </summary>
        public void LevelUp() => Interlocked.Increment(ref _level);

        /// <summary>
        /// Levels down the user (but not less that 1)
        /// </summary>
        public void LevelDown() => Interlocked.Exchange(ref _level, Math.Max(1, _level - 1));

        /// <summary>
        /// Takes a card in hand as face-down
        /// </summary>
        public void TakeInHand(Card card)
        {
            card.Take(this);
            _yourHand.Add(card);
            _backpack.Remove(card);
            _equipped.Remove(card);
        }

        /// <summary>
        /// Puts a card in play as equipped
        /// </summary>
        public void PutInPlayAsEquipped(Card card)
        {
            _equipped.Add(card);
            _backpack.Remove(card);
            _yourHand.Remove(card);
        }

        /// <summary>
        /// Puts a card in play as not equipped
        /// </summary>
        public void PutInPlayAsCarried(Card card)
        {
            _backpack.Add(card);
            _equipped.Remove(card);
            _yourHand.Remove(card);
        }

        /// <summary>
        /// Discards a card from payer
        /// </summary>
        public void Discard(Card card)
        {
            // TODO: make sure that removed cards go into the dicard pile
            _yourHand.Remove(card);
            _backpack.Remove(card);
            _equipped.Remove(card);
        }

        public void DiscardHand()
        {
            // TODO: make sure that removed cards go into the dicard pile
            _yourHand.Clear();
        }

        public void DiscardEquipped()
        {
            // TODO: make sure that removed cards go into the dicard pile
            _equipped.Clear();
        }

        /// <summary>
        /// Revives the players hero
        /// </summary>
        /// <param name="cards"> Cards for hero to start with after being revived. </param>
        public void Revive(IEnumerable<Card> cards)
        {
            cards.ForEach(TakeInHand);
            _isDead = false;
        }

        /// <summary>
        /// Kills the player's hero
        /// </summary>
        public void Kill()
        {
            // TODO: make sure that removed cards go into the dicard pile
            _yourHand.Clear();
            _backpack.Clear();

            var safeCards = Enumerable.Empty<Card>()
                .Concat(_equipped.OfType<RaceCard>())
                .Concat(_equipped.OfType<ClassCard>())
                .Concat(_equipped.OfType<SuperMunchkin>())
                .Concat(_equipped.OfType<Halfbreed>());

            _equipped.Clear();
            _equipped.AddRange(safeCards);
            _isDead = true;
        }
    }
}
