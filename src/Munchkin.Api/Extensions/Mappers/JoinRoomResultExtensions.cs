using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class JoinRoomResultExtensions
    {
        public static GameRoomJoinRoomResultVM ToVM(this JoinRoomResult joinRoomResult)
        {
            return new GameRoomJoinRoomResultVM
            {

            };
        }
    }
}
