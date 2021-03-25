using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using System;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerDecideOnHelpRequest : IRequest<Response<YesNoActions>>
    {
        public PlayerDecideOnHelpRequest(Table table, Player targetPlayer)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            TargetPlayer = targetPlayer ?? throw new ArgumentNullException(nameof(targetPlayer));
        }

        public Table Table { get; }

        public Player TargetPlayer { get; }
    }
}
