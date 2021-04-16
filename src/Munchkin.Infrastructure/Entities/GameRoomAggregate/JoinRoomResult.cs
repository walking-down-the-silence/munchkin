using Munchkin.Core.Contracts;

namespace Munchkin.Infrastructure.Models
{
    public class JoinRoomResult : Enumeration
    {
        private JoinRoomResult(int id, string name) : base(id, name)
        {
        }

        public static JoinRoomResult JoinedRoom => new(1, "Joined The Room");

        public static JoinRoomResult LeftRoom => new(2, "Left The Room");

        public static JoinRoomResult RoomFull => new(3, "Room Is Full");

        public static JoinRoomResult InvalidUser => new(4, "Invalid User");
    }
}
