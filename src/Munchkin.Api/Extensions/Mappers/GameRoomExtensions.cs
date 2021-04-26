using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class GameRoomExtensions
    {
        public static GameRoomVM ToVM(this IGameRoom gameRoom)
        {
            return new GameRoomVM
            {

            };
        }
    }
}
