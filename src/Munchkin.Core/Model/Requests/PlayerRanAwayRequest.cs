using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Requests.Enums;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerRanAwayRequest : IRequest<Response<RanAwayOrContinueActions>>
    {
        public PlayerRanAwayRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }
    }
}
