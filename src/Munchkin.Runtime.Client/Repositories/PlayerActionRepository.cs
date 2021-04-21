using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Runtime.Entities.Actions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Client.Repositories
{
    public class PlayerActionRepository : IPlayerActionRepository
    {
        private readonly Dictionary<Player, List<IAction<Table>>> _playerActions = new();

        public Task AddActionForPlayer(Player player, IAction<Table> action)
        {
            if (player is null)
                throw new System.ArgumentNullException(nameof(player));

            if (action is null)
                throw new System.ArgumentNullException(nameof(action));

            if (!_playerActions.ContainsKey(player)
                || _playerActions[player] == null)
            {
                _playerActions[player] = new List<IAction<Table>>();
            }

            _playerActions[player].Add(action);

            return Task.CompletedTask;
        }

        public Task ClearActionsForPlayer(Player player)
        {
            if (player is null)
                throw new System.ArgumentNullException(nameof(player));

            if (_playerActions.ContainsKey(player))
            {
                _playerActions[player] = null;
            }

            return Task.CompletedTask;
        }

        public Task DeleteActionFromPlayer(Player player, IAction<Table> action)
        {
            if (player is null)
                throw new System.ArgumentNullException(nameof(player));

            if (action is null)
                throw new System.ArgumentNullException(nameof(action));

            if (_playerActions.ContainsKey(player))
            {
                _playerActions[player].Remove(action);
            }

            return Task.CompletedTask;
        }

        public Task<PlayerActionGroup> GetActionsByPlayer(Player player)
        {
            if (player is null)
                throw new System.ArgumentNullException(nameof(player));

            var actions = _playerActions.ContainsKey(player)
                ? _playerActions[player]
                : new List<IAction<Table>>();
            PlayerActionGroup actionCollection = new(player, actions);

            return Task.FromResult(actionCollection);
        }
    }
}
