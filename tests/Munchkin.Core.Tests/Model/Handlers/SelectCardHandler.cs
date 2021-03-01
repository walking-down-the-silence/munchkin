using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Handlers
{
    public class SelectCardHandler : IRequestHandler<PlayerSelectSingleCardRequest, Response<Card>>
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
