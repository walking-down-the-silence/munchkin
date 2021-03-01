using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Takes a card from the doors deck and puts in hand.
    /// </summary>
    public class LootTheRoomStage : State, IStage
    {
        private readonly List<Card> _playedCards;

        public LootTheRoomStage(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();


        public Task<IStage> Resolve(Table table)
        {
            // TODO: check if deck is empty and reshuffle discard if it is
            var doorsCard = table.DoorsCardDeck.Take();
            table.Players.Current.TakeInHand(doorsCard);

            var stage = new CharityStage(_playedCards);
            return Task.FromResult<IStage>(stage);
        }
    }
}
