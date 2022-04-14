using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.UserAggregate
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayerByNicknameAsync(string nickname);

        Task SavePlayerAsync(Player player);
    }
}
