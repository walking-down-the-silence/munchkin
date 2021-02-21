using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

namespace Munchkin.Core.Model.Requests
{
    public class LookForTroubleOrLootTheRoomRequest : IRequest<Response<EmptyRoomActions>>
    {
        public LookForTroubleOrLootTheRoomRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }
    }
}
