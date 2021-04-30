using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class GameRoomExtensions
    {
        public static async Task<GameRoomVM> ToVM(this IGameRoom gameRoom)
        {
            var users = await gameRoom.GetUsers();
            var expansionSelections = await gameRoom.GetExpansionSelections();

            return new GameRoomVM
            {
                Users = users.Select(x => x.ToVM()).ToArray(),
                ExpansionSelections = expansionSelections.Select(x => x.ToVM()).ToArray()
            };
        }
    }
}
