using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Takes a card from the doors deck and puts in hand.
    /// </summary>
    public class LootTheRoomStep : IHierarchialStep<Table>
    {
        private readonly List<Card> _playedCards;

        public LootTheRoomStep(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<Table> Resolve(Table table)
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            var doorsCard = table.DoorsCardDeck.Take();
            table.Players.Current.TakeInHand(doorsCard);

            var stage = new CharityStep(_playedCards);
            return await stage.Resolve(table);
        }
    }
}
