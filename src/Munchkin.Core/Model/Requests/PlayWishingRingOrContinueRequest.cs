using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

namespace Munchkin.Core.Model.Requests
{
    public class PlayWishingRingOrContinueRequest : IRequest<Response<PlayWishingRingOrContinueActions>>
    {
        public PlayWishingRingOrContinueRequest(Player targetPlayer, Table table)
        {
            TargetPlayer = targetPlayer;
            Table = table;
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }
    }
}
