using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private int _level = 1;

        public Player(string name, EGender gender)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            Gender = gender;
            _actions = new List<IActionDefinition<Table>>
            {
                new ActionDefinition<Table>("As For Help", () => new PlayerAskForHelpAction()),
                new ActionDefinition<Table>("Discard A Card", () => new PlayerDiscardCardAction()),
                new ActionDefinition<Table>("End Combat", () => new PlayerEndBattleAction()),
                new ActionDefinition<Table>("End Turn", () => new PlayerEndTurnAction()),
                new ActionDefinition<Table>("Kick Down The Door", () => new PlayerKickDownTheDoorAction()),
                new ActionDefinition<Table>("Look For Trouble", () => new PlayerLookForTroubleAction()),
                new ActionDefinition<Table>("Loot The Room", () => new LootTheRoomAction()),
                new ActionDefinition<Table>("Run Away", () => new PlayerRunAwayAction()),
                new ActionDefinition<Table>("Take Bad Stuff", () => new PlayerTakeBadStuffAction())
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
            if (card is not null)
            {
                card.Take(this);
                _yourHand.Add(card);
                _backpack.Remove(card);
                _equipped.Remove(card);
            }
        }

        /// <summary>
        /// Puts a card in play as equipped
        /// </summary>
        public void PutInPlayAsEquipped(Card card)
        {
            if (card is not null)
            {
                _equipped.Add(card);
                _backpack.Remove(card);
                _yourHand.Remove(card);
            }
        }

        /// <summary>
        /// Puts a card in play as not equipped
        /// </summary>
        public void PutInPlayAsCarried(Card card)
        {
            if (card is not null)
            {
                _backpack.Add(card);
                _equipped.Remove(card);
                _yourHand.Remove(card);
            }
        }

        /// <summary>
        /// Discards a card from the player and puts back into the discard pile.
        /// </summary>
        public void Discard(Table table, Card card)
        {
            if (table is not null && card is not null)
            {
                card.Discard(table);
                _yourHand.Remove(card);
                _backpack.Remove(card);
                _equipped.Remove(card);
            }
        }

        /// <summary>
        /// Discards whole hand from the player and puts back into the discard pile.
        /// </summary>
        /// <param name="table"> Table instance with all the state. </param>
        public void DiscardHand(Table table)
        {
            _yourHand.OfType<DoorsCard>().ForEach(card => card.Discard(table));
            _yourHand.OfType<TreasureCard>().ForEach(card => card.Discard(table));
            _yourHand.Clear();
        }

        /// <summary>
        /// Discards all equipped cards from the player and puts back into the discard pile.
        /// </summary>
        /// <param name="table"> Table instance with all the state. </param>
        public void DiscardEquipped(Table table)
        {
            _equipped.OfType<DoorsCard>().ToList().ForEach(card => card.Discard(table));
            _equipped.OfType<TreasureCard>().ToList().ForEach(card => card.Discard(table));
            _equipped.Clear();
        }

        /// <summary>
        /// Revive the players hero and give intital cards.
        /// </summary>
        /// <param name="table"> Table instance with all the state. </param>
        public void Revive(Table table)
        {
            var doorCards = table.DoorsCardDeck.TakeRange(4);
            var treasureCards = table.TreasureCardDeck.TakeRange(4);

            Enumerable.Empty<Card>()
                .Concat(doorCards)
                .Concat(treasureCards)
                .ForEach(TakeInHand);

            _isDead = false;
        }

        /// <summary>
        /// Kills the player's hero.
        /// </summary>
        /// <param name="table"> Table instance with all the state. </param>
        public async Task Kill(Table table)
        {
            _isDead = true;

            // Step 1: leave the safe cards, level and active curses
            var playerCards = Enumerable.Empty<Card>()
                .Concat(YourHand)
                .Concat(Equipped
                    .NotOfType<RaceCard>()
                    .NotOfType<ClassCard>()
                    .NotOfType<SuperMunchkin>()
                    .NotOfType<Halfbreed>()
                    .NotOfType<CurseCard>())
                .Concat(Backpack)
                .ToList();

            playerCards.ForEach(card => card.Discard(table));


            // TODO: Step 2: send a request to each player to select the cards from dead player

            foreach (var player in table.Players.Where(player => !player.IsDead))
            {
                var selectCardRequest = new PlayerSelectSingleCardRequest(player, table, _yourHand);
                var selectCardResponse = await table.RequestSink.Send(selectCardRequest);
                var selectedCard = await selectCardResponse.Task;

                // TODO: consider how to remove selected card from pool of options
                _yourHand.Remove(selectedCard);
                player.TakeInHand(selectedCard);
            }

            _yourHand.Clear();
            _backpack.Clear();
        }
    }
}
