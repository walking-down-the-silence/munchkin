using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using System;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerSelectSinglePlayerRequest : IRequest<Response<Player>>
    {
        public PlayerSelectSinglePlayerRequest(Table table, Player targetPlayer, IReadOnlyCollection<Player> options)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            TargetPlayer = targetPlayer ?? throw new ArgumentNullException(nameof(targetPlayer));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Table Table { get; }

        public Player TargetPlayer { get; }

        public IReadOnlyCollection<Player> Options { get; }
    }
}
