using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Requests.Enums;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerDiscardHandOrLoose2LevelsRequest : IRequest<Response<DiscardHandOrLoose2LevelsActions>>
    {
        public PlayerDiscardHandOrLoose2LevelsRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; set; }

        public Table Table { get; set; }
    }
}
