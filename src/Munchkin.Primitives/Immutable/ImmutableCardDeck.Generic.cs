using Munchkin.Primitives.Abstractions;
using System.Collections;
using System.Collections.Immutable;

namespace Munchkin.Primitives.Immutable
{
    public record ImmutableCardDeck<TCard> : IEnumerable<TCard>
    {
        private readonly IShuffleAlgorithm<TCard> _shuffleAlgorithm;
        private ImmutableList<TCard> _cards;

        public ImmutableCardDeck(IShuffleAlgorithm<TCard> shuffleAlgorithm, ImmutableList<TCard> cards)
        {
            _shuffleAlgorithm = shuffleAlgorithm ?? throw new ArgumentNullException(nameof(shuffleAlgorithm));
            _cards = cards ?? throw new ArgumentNullException(nameof(cards));
        }

        public static ImmutableCardDeck<TCard> Empty = new(new DefaultShuffleAlgorithm<TCard>(), ImmutableList<TCard>.Empty);

        public bool IsEmpty => _cards.Count == 0;

        public override string ToString() => $"Count = {_cards.Count}";

        public ImmutableCardDeck<TCard> Shuffle()
        {
            var shuffled = _cards.ToArray();
            _shuffleAlgorithm.Shuffle(shuffled);

            return this with { _cards = ImmutableList.CreateRange(shuffled) };
        }

        public ImmutableCardDeck<TCard> Take(out TCard card)
        {
            card = _cards.LastOrDefault();
            return this with { _cards = _cards.Remove(card) };
        }

        public ImmutableCardDeck<TCard> Peek(int index, out TCard card)
        {
            card = _cards[index];
            return this;
        }

        public ImmutableCardDeck<TCard> TakeRange(int count, out IEnumerable<TCard> cards)
        {
            cards = _cards.TakeLast(count);
            return this with { _cards = _cards.RemoveRange(cards) };
        }

        public ImmutableCardDeck<TCard> TakeFirst<TResult>(out TResult card) where TResult : TCard
        {
            card = _cards.OfType<TResult>().LastOrDefault();
            return this with { _cards = _cards.Remove(card) };
        }

        public ImmutableCardDeck<TCard> TakeLast<TResult>(out TResult card) where TResult : TCard
        {
            card = _cards.OfType<TResult>().FirstOrDefault();
            return this with { _cards = _cards.Remove(card) };
        }

        public ImmutableCardDeck<TCard> Put(TCard card)
        {
            return this with { _cards = _cards.Add(card) };
        }

        public ImmutableCardDeck<TCard> PutRange(IEnumerable<TCard> items)
        {
            return this with { _cards = _cards.AddRange(items) };
        }

        public IEnumerator<TCard> GetEnumerator() => _cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
