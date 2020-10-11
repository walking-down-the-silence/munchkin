using MediatR;
using Munchkin.Core.Model.Cards;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Requests
{
    public class SelectCardRequest : IRequest<Card>
    {
        public SelectCardRequest(Player targetPlayer, Table table, IReadOnlyCollection<Card> options)
        {
            TargetPlayer = targetPlayer ?? throw new System.ArgumentNullException(nameof(targetPlayer));
            Table = table ?? throw new System.ArgumentNullException(nameof(table));
            Options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }

        public IReadOnlyCollection<Card> Options { get; }
    }

    public class SelectCardHandler : IRequestHandler<SelectCardRequest, Card>
    {
        public Task<Card> Handle(SelectCardRequest request, CancellationToken cancellationToken)
        {
            // NOTE: a simulation of the actual selection of the card
            var selectedCard = request.TargetPlayer.YourHand.FirstOrDefault();
            request.Table.Dungeon.PutInPlay(selectedCard);
            return Task.FromResult(selectedCard);
        }
    }

    public class DiscardHandOrLoose2LevelsRequest : IRequest
    {

    }

    public class DiscardHandOrLoose2LevelsHandler : IRequestHandler<DiscardHandOrLoose2LevelsRequest>
    {
        public Task<Unit> Handle(DiscardHandOrLoose2LevelsRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DiceRollRequest : IRequest<int>
    {
        public DiceRollRequest(Player player)
        {
            Player = player ?? throw new System.ArgumentNullException(nameof(player));
        }

        public Player Player { get; }
    }
}
