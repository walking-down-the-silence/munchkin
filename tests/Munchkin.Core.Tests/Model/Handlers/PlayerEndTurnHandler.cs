using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Model.Handlers
{
    public sealed class PlayerEndTurnHandler : IRequestHandler<PlayerEndTurnRequest, Response<Unit>>
    {
        public async Task<Response<Unit>> Handle(PlayerEndTurnRequest request, CancellationToken cancellationToken)
        {
            // NOTE: this implementation autoresolves the action and is purely for testing purpose
            var (source, response) = Response<Unit>.Create();
            var playerAction = new PlayerNextStageAction(source);
            await playerAction.ExecuteAsync(request.Table);
            return response;
        }
    }
}
