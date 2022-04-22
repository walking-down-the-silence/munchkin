using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Model.Handlers
{
    internal class GameWaitForPlayerHandler : IRequestHandler<GameWaitForPlayerRequest, Response<Unit>>
    {
        public Task<Response<Unit>> Handle(GameWaitForPlayerRequest request, CancellationToken cancellationToken)
        {
            // NOTE: this implementation autoresolves the action and is purely for testing purpose
            var (source, response) = Response<Unit>.Create();
            source.SetResult(Unit.Value);
            return Task.FromResult(response);
        }
    }
}
