using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;

namespace Munchkin.Core.Model.Requests
{
    public sealed class PlayerEndTurnRequest : IRequest<Response<Unit>>
    {
        public PlayerEndTurnRequest(Table table, Player targetPlayer)
        {
            Table = table ?? throw new System.ArgumentNullException(nameof(table));
            TargetPlayer = targetPlayer ?? throw new System.ArgumentNullException(nameof(targetPlayer));
        }

        public Table Table { get; }

        public Player TargetPlayer { get; }
    }
}
