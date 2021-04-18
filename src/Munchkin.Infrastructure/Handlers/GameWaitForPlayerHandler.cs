using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Requests;
using Munchkin.Runtime.Entities.Actions;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Infrastructure.Handlers
{
    internal class GameWaitForPlayerHandler : IRequestHandler<GameWaitForPlayerRequest, Response<Unit>>
    {
        private readonly IPlayerActionRepository _playerActionRepository;

        public GameWaitForPlayerHandler(IPlayerActionRepository playerActionRepository)
        {
            _playerActionRepository = playerActionRepository ?? throw new System.ArgumentNullException(nameof(playerActionRepository));
        }

        public async Task<Response<Unit>> Handle(GameWaitForPlayerRequest request, CancellationToken cancellationToken)
        {
            var (source, response) = Response<Unit>.Create();
            var playerAction = new PlayerNextStageAction(source);

            await _playerActionRepository.AddActionForPlayer(request.TargetPlayer, playerAction);
            return response;
        }
    }
}
