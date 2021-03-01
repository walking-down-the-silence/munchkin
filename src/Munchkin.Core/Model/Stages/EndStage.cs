﻿using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EndStage : State, IStage
    {
        private readonly List<Card> _playedCards;

        public EndStage(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => true;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public Task<IStage> Resolve(Table table)
        {
            _playedCards.ForEach(card => card.Discard(table));
            _playedCards.Clear();
            return Task.FromResult((IStage)this);
        }
    }
}
