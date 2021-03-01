using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Stages;
using System.Collections.Generic;
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
        private IStage _currentStage;

        public Dungeon(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public IStage CurrentStage => _currentStage;

        /// <summary>
        /// Allows main players hero to enter the dungeon
        /// </summary>
        /// <param name="door"> Doors, in which hero should enter the dungeon. </param>
        /// <param name="table"> Table state when entering the dungeon. </param>
        public DoorsCard KickOpenTheDoor()
        {
            var playerActions = _table.Players.Current.Actions.Select(action => action.Create()).ToList();
            SetPlayerActions(_table.Players.Current, playerActions);

            var door = _table.DoorsCardDeck.Take();
            return door;
        }

        public async Task<bool> MoveToNextStage(Table table)
        {
            _currentStage = await _currentStage.Resolve(table);
            return _currentStage.IsTerminal;
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