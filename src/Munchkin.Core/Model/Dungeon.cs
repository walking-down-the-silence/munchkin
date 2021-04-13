using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class Dungeon : State
    {
        private readonly Table _table;
        private readonly List<Card> _playedCards = new();

        public Dungeon(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public override void Reset()
        {
            base.Reset();
            _playedCards.Clear();
        }

        public void AddPlayedCard(Card card) => _playedCards.Add(card);

        public void RemovePlayedCard(Card card) => _playedCards.Remove(card);

        public async Task<Table> WaitForAllPlayers()
        {
            // NOTE: map each player to their own Task Completion Source, so that they can end combat
            var playerResponses = _table.Players
                .Select(player => new GameWaitForPlayerRequest(_table, player))
                .Select(request => _table.RequestSink.Send(request));

            // NOTE: select and wait for all players to end combat
            await Task.WhenAll(playerResponses);

            return _table;
        }
    }
}