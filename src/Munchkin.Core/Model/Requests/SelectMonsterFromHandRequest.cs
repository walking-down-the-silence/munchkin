using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model.Cards;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Requests
{
    public class SelectMonsterFromHandRequest : IRequest<Response<MonsterCard>>
    {
        public SelectMonsterFromHandRequest(Player targetPlayer, Table table, IReadOnlyCollection<MonsterCard> options)
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
