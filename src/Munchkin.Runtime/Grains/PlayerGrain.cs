using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Grains
{
    public class PlayerGrain : Grain, IPlayer
    {
        private readonly IPersistentState<Player> _playerPersistance;

        public PlayerGrain(
            [PersistentState("player", "playerStore")] IPersistentState<Player> playerPersistance)
        {
            _playerPersistance = playerPersistance ?? throw new ArgumentNullException(nameof(playerPersistance));
        }

        public Task<Player> GetStateAsync() => Task.FromResult(_playerPersistance.State);
    }
}
