using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.PlayerInteraction;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerSelectMonsterFromHandRequest : IRequest<Response<MonsterCard>>
    {
        public PlayerSelectMonsterFromHandRequest(Player targetPlayer, Table table, IReadOnlyCollection<MonsterCard> options)
        {
            TargetPlayer = targetPlayer;
            Table = table;
            Options = options;
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }

        public IReadOnlyCollection<MonsterCard> Options { get; }
    }
}
