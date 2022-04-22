using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Requests.Enums;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerLookForTroubleOrLootTheRoomRequest : IRequest<Response<EmptyRoomActions>>
    {
        public PlayerLookForTroubleOrLootTheRoomRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }
    }
}
