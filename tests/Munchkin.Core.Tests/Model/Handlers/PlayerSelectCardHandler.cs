using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Model.Handlers
{
    public class PlayerSelectCardHandler : IRequestHandler<PlayerSelectSingleCardRequest, Response<Card>>
    {
        public Task<Response<Card>> Handle(PlayerSelectSingleCardRequest request, CancellationToken cancellationToken)
        {
            var (source, response) = Response<Card>.Create();
            var selectedCard = request.TargetPlayer.YourHand.FirstOrDefault();
            source.SetResult(selectedCard);
            return Task.FromResult(response);
        }
    }
}
