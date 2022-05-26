using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Cards.Doors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Represent the player in the game with a name, level and other attributes.
    /// </summary>
    public class Player : IEquatable<Player>
    {
        private readonly List<Card> _yourHand = new();
        private readonly List<Card> _backpack = new();
        private readonly List<Card> _equipped = new();
        private readonly List<IAction<Table>> _actions = new();
        private EPlayerLifeState _life;
        private int _level = 1;

        public Player(string nickname, EGender gender)
        {
            if (string.IsNullOrWhiteSpace(nickname))
                throw new ArgumentException($"'{nameof(nickname)}' cannot be null or whitespace.", nameof(nickname));

            Nickname = nickname;
            Gender = gender;
        }

        /// <summary>
        /// Gets the players name.
        /// </summary>
        public string Nickname { get; }

        /// <summary>
        /// Gets the players gender.
        /// </summary>
        public EGender Gender { get; }

        /// <summary>
        /// Gets the players current level.
        /// </summary>
        public int Level => _level;

        /// <summary>
        /// Gets the total strength of all equipped items.
        /// </summary>
        public int Strength => _equipped.OfType<ItemCard>().Aggregate(0, (total, item) => total + item.StrengthBonus);

        /// <summary>
        /// Gets if the players hero has recently died, was being revived or is alive.
        /// </summary>
        public EPlayerLifeState Life => _life;

        /// <summary>
        /// Gets the closed out-of-game cards.
        /// </summary>
        public IReadOnlyCollection<Card> YourHand => _yourHand;

        /// <summary>
        /// Gets that are in play and are equipped.
        /// </summary>
        public IReadOnlyCollection<Card> Equipped => _equipped;

        /// <summary>
        /// Gets that are in play but are not equipped.
        /// </summary>
        public IReadOnlyCollection<Card> Backpack => _backpack;

        /// <summary>
        /// Gets the dynamic actions for the user.
        /// </summary>
        public IReadOnlyCollection<IAction<Table>> Actions => _actions.AsReadOnly();

        /// <inheritdoc />
        public override string ToString() => $"{Nickname}, Level {Level}, {Life}";

        /// <inheritdoc />
        public bool Equals(Player other) => string.Equals(Nickname, other?.Nickname, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Levels up the player.
        /// </summary>
        public void LevelUp() => Interlocked.Increment(ref _level);

        /// <summary>
        /// Levels up the player by a couple levels.
        /// </summary>
        public void LevelUp(int levels) => Interlocked.Add(ref _level, levels);

        /// <summary>
        /// Levels down the user (but not less that 1).
        /// </summary>
        public void LevelDown() => Interlocked.Exchange(ref _level, Math.Max(1, _level - 1));

        /// <summary>
        /// Levels down the user  by couple levels (but not less that 1).
        /// </summary>
        public void LevelDown(int levels) => Interlocked.Exchange(ref _level, Math.Max(1, _level - levels));

        /// <summary>
        /// Takes a card in hand as face-down.
        /// </summary>
        public void TakeInHand(Card card)
        {
            if (card is not null)
            {
                card.TakenBy(this);
                _yourHand.Add(card);
                _backpack.Remove(card);
                _equipped.Remove(card);
            }
        }

        /// <summary>
        /// Puts a card in play as equipped.
        /// </summary>
        public void Equip(Card card)
        {
            if (card is not null)
            {
                card.TakenBy(this);
                _equipped.Add(card);
                _backpack.Remove(card);
                _yourHand.Remove(card);
            }
        }

        /// <summary>
        /// Puts a card in play as not equipped.
        /// </summary>
        public void PutInBackpack(Card card)
        {
            if (card is not null)
            {
                card.TakenBy(this);
                _backpack.Add(card);
                _equipped.Remove(card);
                _yourHand.Remove(card);
            }
        }

        /// <summary>
        /// Discards a card from the player and puts back into the discard pile.
        /// </summary>
        public void Discard(Card card)
        {
            if (card is not null)
            {
                _yourHand.Remove(card);
                _backpack.Remove(card);
                _equipped.Remove(card);
            }
        }

        /// <summary>
        /// Discards whole hand from the player and puts back into the discard pile.
        /// </summary>
        /// <param name="table"> Table instance with all the state. </param>
        public void DiscardHand() => _yourHand.Clear();

        /// <summary>
        /// Discards all equipped cards from the player and puts back into the discard pile.
        /// </summary>
        public void DiscardEquipped() => _equipped.Clear();

        /// <summary>
        /// On your next turn, start by drawing four face-down cards from each deck
        /// and playing any legal cards you want to, just as when you started the game.
        /// Then take your turn normally.
        /// </summary>
        /// <param name="doors">The 4 Door cards to take into hand.</param>
        /// <param name="treasures">The 4 Treasure cards to take into hand.</param>
        public void ReceiveCards(IReadOnlyCollection<DoorsCard> doors, IReadOnlyCollection<TreasureCard> treasures)
        {
            ArgumentNullException.ThrowIfNull(doors, nameof(doors));
            ArgumentNullException.ThrowIfNull(treasures, nameof(treasures));

            if (!doors.Any())
                throw new ArgumentException("Player should be revived with 4 door cards.", nameof(doors));

            if (!treasures.Any())
                throw new ArgumentException("Player should be revived with 4 treasure cards.", nameof(treasures));

            doors.ForEach(TakeInHand);
            treasures.ForEach(TakeInHand);

            _life = EPlayerLifeState.Alive;
        }

        /// <summary>
        /// When the next player begins his turn, your new character appears and can
        /// help others in combat with his Level and Class or Race abilities. . .but you
        /// have no cards, unless you receive Charity or gifts from other players.
        /// </summary>
        public void Revive() => _life = EPlayerLifeState.Revived;

        /// <summary>
        /// If you die, you lose all your stuff. You keep your Class(es), Race(s),
        /// and Level(and any Curses that were affecting you when you died) – your
        /// new character will look just like your old one.If you have Half-Breed or
        /// Super Munchkin, keep those as well.
        /// </summary>
        /// <returns> A collection of cards that should be discarded. </returns>
        public IReadOnlyCollection<Card> Die()
        {
            _life = EPlayerLifeState.Dead;

            var playerCards = Enumerable.Empty<Card>()
                .Concat(YourHand)
                .Concat(Equipped
                    .ExceptType<ClassCard>()
                    .ExceptType<RaceCard>()
                    .ExceptType<CurseCard>()
                    .ExceptType<Halfbreed>()
                    .ExceptType<SuperMunchkin>())
                .Concat(Backpack)
                .ToList();

            _yourHand.Clear();
            _backpack.Clear();

            return playerCards;
        }
    }
}
