using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CharityStage : State, IStage
    {
        private readonly List<Card> _playedCards;

        public CharityStage(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public Task<IStage> Resolve(Table table)
        {
            var stage = new EndStage(_playedCards);
            return Task.FromResult<IStage>(stage);
        }
    }
}
