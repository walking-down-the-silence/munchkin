using Munchkin.Infrastructure.Entities.UserAggregate;
using Munchkin.Infrastructure.Models;
using System;

namespace Munchkin.Infrastructure.Services
{
    public class GameRoomService
    {
        public GameRoom CreateRoom(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var gameRoom = new GameRoom();
            var (result, joinResponse) = gameRoom.JoinRoom(user);
            return joinResponse == JoinRoomResult.JoinedRoom ? result : default;
        }
    }
}
