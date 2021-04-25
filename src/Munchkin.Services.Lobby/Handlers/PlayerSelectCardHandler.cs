using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Handlers
{
    public class PlayerSelectCardHandler : IRequestHandler<PlayerSelectSingleCardRequest, Response<Card>>
    {
        public Task<Response<Card>> Handle(PlayerSelectSingleCardRequest request, CancellationToken cancellationToken)
        {
            var (source, response) = Response<Card>.Create();
            //TODO: bind card options to UI and implement a handler that will set selected card as result
            return Task.FromResult(response);
        }
    }
}
