using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

namespace Munchkin.Core.Model.Requests
{
    public class RollTheDiceRequest : IRequest<Response<int>>
    {
        public RollTheDiceRequest(Player player)
        {
            Player = player ?? throw new System.ArgumentNullException(nameof(player));
        }

        public Player Player { get; }
    }
}
