using Munchkin.Runtime.Abstractions.UserAggregate;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public interface IGameRoom : IGrainWithIntegerKey
    {
        Task<bool> IsEmpty();

        Task<IReadOnlyCollection<User>> GetUsers();

        Task<JoinRoomResult> JoinRoom(User user);

        Task<JoinRoomResult> LeaveRoom(User player);

        Task SetAvailableExpansions(ExpansionOption[] avaialableExpansions);

        Task<IReadOnlyCollection<ExpansionSelection>> GetExpansionSelections();

        Task<SelectExpansionResult> SelectExpansion(string code);

        Task<SelectExpansionResult> UnselectExpansion(string code);
    }
}
