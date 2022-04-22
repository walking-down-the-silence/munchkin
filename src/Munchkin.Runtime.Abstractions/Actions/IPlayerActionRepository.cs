using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.Actions
{
    public interface IPlayerActionRepository
    {
        Task AddActionForPlayer(Player player, IAction<Table> action);

        Task ClearActionsForPlayer(Player player);

        Task DeleteActionFromPlayer(Player player, IAction<Table> action);

        Task<PlayerActionGroup> GetActionsByPlayer(Player player);
    }
}
