using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;

namespace Munchkin.Core.Model
{
    public class Dungeon : State
    {
        private readonly List<Card> _playedCards = new();

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public override void Reset()
        {
            base.Reset();
            _playedCards.Clear();
        }

        public void AddPlayedCard(Card card) => _playedCards.Add(card);

        public void RemovePlayedCard(Card card) => _playedCards.Remove(card);
    }
}