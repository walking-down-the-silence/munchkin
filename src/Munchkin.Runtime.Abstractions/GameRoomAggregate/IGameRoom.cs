using Munchkin.Runtime.Abstractions.UserAggregate;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public interface IGameRoom : IGrainWithIntegerKey
    {
        Task<bool> IsEmpty();

        Task<IReadOnlyCollection<User>> GetPlayers();

        Task<JoinRoomResult> JoinRoom(User user);

        Task<JoinRoomResult> LeaveRoom(User player);

        Task<IReadOnlyCollection<ExpansionOption>> GetSelectedExpansions();

        Task<IGameRoom> SetAvailableExpansions(ExpansionOption[] avaialableExpansions);

        Task<SelectExpansionResult> SelectExpansion(string code);

        Task<SelectExpansionResult> UnselectExpansion(string code);
    }
}
