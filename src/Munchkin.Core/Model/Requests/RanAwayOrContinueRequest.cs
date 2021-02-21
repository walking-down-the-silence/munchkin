using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

namespace Munchkin.Core.Model.Requests
{
    public class RanAwayOrContinueRequest : IRequest<Response<RanAwayOrContinueActions>>
    {
        public RanAwayOrContinueRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; }

        public Table Talbe { get; }
        public Table Table { get; }
    }
}
