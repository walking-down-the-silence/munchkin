using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Handlers
{
    public class DiscardHandOrLoose2LevelsHandler : IRequestHandler<PlayerDiscardHandOrLoose2LevelsRequest, Response<DiscardHandOrLoose2LevelsActions>>
    {
        public Task<Response<DiscardHandOrLoose2LevelsActions>> Handle(PlayerDiscardHandOrLoose2LevelsRequest request, CancellationToken cancellationToken)
        {
            var (source, response) = Response<DiscardHandOrLoose2LevelsActions>.Create();
            source.SetResult(DiscardHandOrLoose2LevelsActions.DiscardHand);
            return Task.FromResult(response);
        }
    }
}
