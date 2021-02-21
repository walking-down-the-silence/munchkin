using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

namespace Munchkin.Core.Model.Requests
{
    public class DiscardHandOrLoose2LevelsRequest : IRequest<Response<DiscardHandOrLoose2LevelsActions>>
    {
        public DiscardHandOrLoose2LevelsRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; set; }

        public Table Table { get; set; }
    }
}
