using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EndStage : State, IStage
    {
        private readonly Table _table;
        private readonly List<Card> _playedCards;

        public EndStage(Table table, List<Card> playedCards)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => true;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public Task<IStage> Resolve()
        {
            _playedCards.ForEach(card => card.Discard(_table));
            _playedCards.Clear();
            return Task.FromResult((IStage)this);
        }
    }
}
