using Munchkin.Api.ViewModels;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class CardExtensions
    {
        public static CardVM ToVM(this Card card)
        {
            return new CardVM
            {
                CardId = 0,
                Title = card.Title,
                ImageUrl = string.Empty,
                Owner = new CardOwnerVM
                {
                    OwnerId = 0,
                    Name = card.Owner?.Nickname ?? "<null>"
                }
            };
        }
    }
}
