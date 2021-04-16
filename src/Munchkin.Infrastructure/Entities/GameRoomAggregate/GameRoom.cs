using Munchkin.Infrastructure.Entities.UserAggregate;
using System.Collections.Generic;

namespace Munchkin.Infrastructure.Models
{
    public class GameRoom
    {
        private readonly List<User> _players = new();

        private GameRoom()
        {
        }

        public static GameRoom Create(User user)
        {
            if (user is null)
            {
                throw new System.ArgumentNullException(nameof(user));
            }

            var gameRoom = new GameRoom();
            var joinResponse = gameRoom.JoinRoom(user);
            return gameRoom;
        }

        public bool IsEmpty => _players.Count == 0;

        public IReadOnlyCollection<User> Players => _players;

        public JoinRoomResult JoinRoom(User player)
        {
            if (player is null)
            {
                return JoinRoomResult.InvalidUser;
            }

            _players.Add(player);
            return JoinRoomResult.JoinedRoom;
        }

        public JoinRoomResult LeaveRoom(User player)
        {
            if (player is null)
            {
                return JoinRoomResult.InvalidUser;
            }

            if (_players.Remove(player))
            {
                return JoinRoomResult.LeftRoom;
            }

            return JoinRoomResult.InvalidUser;
        }
    }
}
