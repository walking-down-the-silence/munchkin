using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public class EmptyRoomActions : Enumeration
    {
        private EmptyRoomActions(int id, string name) : base(id, name)
        {
        }

        public static EmptyRoomActions LookForTrouble => new EmptyRoomActions(1, "Look For Trouble");

        public static EmptyRoomActions LootTheRoom => new EmptyRoomActions(2, "Loot The Room");
    }
}
