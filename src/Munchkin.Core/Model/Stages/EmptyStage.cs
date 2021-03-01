﻿using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EmptyStage : State, IStage
    {
        private readonly List<Card> _playedCards;

        public EmptyStage(List<Card> playedCards)
        {
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<IStage> Resolve(Table table)
        {
            var request = new PlayerLookForTroubleOrLootTheRoomRequest(table.Players.Current, table);
            var response = await table.RequestSink.Send(request);
            var action = await response.Task;

            return action == EmptyRoomActions.LookForTrouble
                ? new LookForTroubleStage(_playedCards)
                : new LootTheRoomStage(_playedCards);
        }
    }
}
