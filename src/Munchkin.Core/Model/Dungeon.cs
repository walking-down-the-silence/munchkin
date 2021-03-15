using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.States;
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
        private readonly Dictionary<Player, List<IAction<Table>>> _playerActions = new();

        public Dungeon(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

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
                table.Dungeon.Clear();
                table.Players.Next();
            }

            return table;
        }

        public async Task OngoingCombat()
        {
            // TODO: send requests to each player to store the Task Completion Source used to end the combat

            // NOTE: map each player to their own Task Completion Source, so that they can end combat
            var playerTurnTasks = _table.Players.Select(player => (player, tcs: new TaskCompletionSource()));

            // NOTE: select and wait for all players to end combat
            var aggregatedTasks = playerTurnTasks.Select(x => x.tcs.Task);
            await Task.WhenAll(aggregatedTasks);
        }

        public void SetPlayerActions(Player player, ICollection<IAction<Table>> actions)
        {
            _playerActions[player] = actions.ToList();
        }

        public IReadOnlyCollection<IAction<Table>> GetPlayerActions(Player player)
        {
            return _playerActions[player].AsReadOnly();
        }
    }
}