using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

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
