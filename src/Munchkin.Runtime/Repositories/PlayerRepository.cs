using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly Dictionary<string, Player> _players = new();

        public Task<Player> GetPlayerByNicknameAsync(string nickname)
        {
            return _players.ContainsKey(nickname)
                ? Task.FromResult(_players[nickname])
                : Task.FromResult<Player>(null);
        }

        public Task<IReadOnlyCollection<Player>> GetPlayersAsync(string tableId)
        {
            return Task.FromResult<IReadOnlyCollection<Player>>(_players.Values);
        }

        public Task SavePlayerAsync(Player player)
        {
            _ = player is not null
                ? (_players[player.Nickname] = player)
                : null;
            return Task.CompletedTask;
        }
    }
}
