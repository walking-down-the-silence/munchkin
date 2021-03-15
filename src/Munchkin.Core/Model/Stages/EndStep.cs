using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EndStep : IStep<Table>
    {
        private readonly List<Card> _playedCards;

        public EndStep(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public Task<Table> Resolve(Table table)
        {
            _playedCards.ForEach(card => card.Discard(table));
            _playedCards.Clear();
            return Task.FromResult(table);
        }
    }
}
