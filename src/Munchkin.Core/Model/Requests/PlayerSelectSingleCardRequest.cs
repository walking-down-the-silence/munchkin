using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerSelectSingleCardRequest : IRequest<Response<Card>>
    {
        public PlayerSelectSingleCardRequest(Player targetPlayer, Table table, IReadOnlyCollection<Card> options)
        {
            TargetPlayer = targetPlayer ?? throw new System.ArgumentNullException(nameof(targetPlayer));
            Table = table ?? throw new System.ArgumentNullException(nameof(table));
            Options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }

        public IReadOnlyCollection<Card> Options { get; }
    }
}
