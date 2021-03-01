using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Requests.Enums;

namespace Munchkin.Core.Model.Requests
{
    public class LoseItemsOrCardsInHandRequest : IRequest<Response<LoseItemsOrCardsInHandActions>>
    {
        public LoseItemsOrCardsInHandRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; set; }

        public Table Table { get; set; }
    }
}
