using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Stages;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Dungeon state representing all goods in it
    /// </summary>
    public class Dungeon : State
    {
        private readonly Table _table;
        private readonly List<Card> _playedCards = new();

        public Dungeon(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public static async Task<Table> NextTurn()
        {
            var stage = new SetupTableStep();
            var table = await stage.Resolve(new Table(null));
            var history = ImmutableStack<Table>.Empty;

            while (!table.IsGameWon)
            {
                var playerTurn = new PlayerTurnStep();
                table = await playerTurn.Resolve(table);

                history = history.Push(table);

                // NOTE: clear/reset the state befor moving to next turn
                table.Dungeon.Reset();
                table.Players.Next();
            }

            return table;
        }

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