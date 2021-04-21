using Munchkin.Core.Contracts;

namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public class JoinRoomResult : Enumeration
    {
        private JoinRoomResult(int id, string name) : base(id, name)
        {
        }

        public static JoinRoomResult JoinedRoom => new(1, "Joined The Room");

        public static JoinRoomResult LeftRoom => new(2, "Left The Room");

        public static JoinRoomResult RoomFull => new(3, "Room Is Full");

        public static JoinRoomResult RoomEmpty => new(4, "Room Is Empty");

        public static JoinRoomResult InvalidUser => new(5, "Invalid User");
    }
}
