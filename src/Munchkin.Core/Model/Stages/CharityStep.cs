using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CharityStep : IHierarchialStep<Table>
    {
        private readonly List<Card> _playedCards;

        public CharityStep(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<Table> Resolve(Table table)
        {
            // TODO: implement the charity loop
            var stage = new EndStep(_playedCards);
            return await stage.Resolve(table);
        }
    }
}
