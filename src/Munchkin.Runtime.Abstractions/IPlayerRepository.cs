using Munchkin.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayerByNicknameAsync(string nickname);

        Task<IReadOnlyCollection<Player>> GetPlayersAsync(string tableId);

        Task SavePlayerAsync(Player player);
    }
}
