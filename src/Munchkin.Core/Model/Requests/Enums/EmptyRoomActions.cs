using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public sealed record EmptyRoomActions : Enumeration
    {
        private EmptyRoomActions(int code, string name) : base(code, name)
        {
        }

        public static EmptyRoomActions LookForTrouble => new EmptyRoomActions(1, "Look For Trouble");

        public static EmptyRoomActions LootTheRoom => new EmptyRoomActions(2, "Loot The Room");
    }
}
