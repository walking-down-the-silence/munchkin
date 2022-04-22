using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core
{
    public class CardDeck<TCard> : ICardDeck<TCard> where TCard : Card
    {
        private readonly List<TCard> _cards;

        public CardDeck()
        {
            _cards = new List<TCard>();
        }

        public CardDeck(IEnumerable<TCard> cards)
        {
            _cards = new List<TCard>(cards);
        }

        public bool IsEmpty => _cards.Count == 0;

        public override string ToString() => $"Count = {_cards.Count}";

        public void Shuffle()
        {
            int count = _cards.Count;
            var random = new Random((int)DateTime.Now.Ticks);
            while (count > 1)
            {
                count--;
                int randomNumber = random.Next(count + 1);
                TCard value = _cards[randomNumber];
                _cards[randomNumber] = _cards[count];
                _cards[count] = value;
            }
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
