using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.PlayerInteraction;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Requests
{
    public class PlayerSelectMultipleCardsRequest : IRequest<Response<ICollection<Card>>>
    {
        public PlayerSelectMultipleCardsRequest(Player targetPlayer, Table table, IReadOnlyCollection<Card> options, int cardsToSelectQuantity)
        {
            TargetPlayer = targetPlayer ?? throw new System.ArgumentNullException(nameof(targetPlayer));
            Table = table ?? throw new System.ArgumentNullException(nameof(table));
            Options = options ?? throw new System.ArgumentNullException(nameof(options));
            CardsToSelectQuantity = cardsToSelectQuantity; // TODO: should I check for 0?
        }

        public Player TargetPlayer { get; }

        public Table Table { get; }

        public IReadOnlyCollection<Card> Options { get; }

        public int CardsToSelectQuantity { get; }
    }
}
