using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EmptyRoomStep : IHierarchialStep<Table>
    {
        private readonly List<Card> _playedCards;

        public EmptyRoomStep(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<Table> Resolve(Table table)
        {
            var request = new PlayerLookForTroubleOrLootTheRoomRequest(table.Players.Current, table);
            var response = await table.RequestSink.Send(request);
            var action = await response.Task;

            IStep<Table> lookForTrouble = new LookForTroubleStep(_playedCards);
            IStep<Table> lootTheRoom = new LootTheRoomStep(_playedCards);
            var stage = action == EmptyRoomActions.LookForTrouble
                ? lookForTrouble
                : lootTheRoom;
            return await stage.Resolve(table);
        }
    }
}
