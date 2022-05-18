using Munchkin.Primitives.Abstractions;
using System.Collections;

namespace Munchkin.Primitives
{
    public class CardDeck<TCard> : ICardDeck<TCard>
    {
        private readonly List<TCard> _cards;
        private readonly IShuffleAlgorithm<TCard> _shuffleAlgorithm;

        public CardDeck(IShuffleAlgorithm<TCard> shuffleAlgorithm, IEnumerable<TCard> cards)
        {
            _shuffleAlgorithm = shuffleAlgorithm ?? new DefaultShuffleAlgorithm<TCard>();
            _cards = new List<TCard>(cards);
        }

        public CardDeck(IShuffleAlgorithm<TCard> shuffleAlgorithm) : this(shuffleAlgorithm, Enumerable.Empty<TCard>())
        {
        }

        public CardDeck(IEnumerable<TCard> cards) : this(default, cards)
        {
        }

        public CardDeck() : this(default, Enumerable.Empty<TCard>())
        {
        }

        public bool IsEmpty => _cards.Count == 0;

        public override string ToString() => $"Count = {_cards.Count}";

        public void Shuffle()
        {
            var shuffled = _cards.ToArray();
            _shuffleAlgorithm.Shuffle(shuffled);
            _cards.Clear();
            _cards.AddRange(shuffled);
        }

        public TCard Take()
        {
            var last = _cards.LastOrDefault();
            if (last != null)
            {
                _cards.Remove(last);
                return last;
            }

            return default;
        }

        public TCard Peek(int index) => _cards[index];

        public IEnumerable<TCard> TakeRange(int count) => Enumerable.Range(0, count).Select(item => Take());

        public TResult TakeFirst<TResult>() where TResult : TCard
        {
            var found = _cards.OfType<TResult>().LastOrDefault();
            if (found != null)
            {
                _cards.Remove(found);
                return found;
            }

            return default;
        }

        public TResult TakeLast<TResult>() where TResult : TCard
        {
            var found = _cards.OfType<TResult>().FirstOrDefault();
            if (found != null)
            {
                _cards.Remove(found);
                return found;
            }

            return default;
        }

        public void Put(TCard card) => _cards.Add(card);

        public void PutRange(IEnumerable<TCard> items) => _cards.AddRange(items);

        public IEnumerator<TCard> GetEnumerator() => _cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
