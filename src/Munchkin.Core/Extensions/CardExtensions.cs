using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class CardExtensions
    {
        public static IEnumerable<Card> NotOfType<TExcept>(this IEnumerable<Card> list)
        {
            return list.Where(card => card is not TExcept);
        }

        public static bool HasAttribute<TAttribute>(this Card card) where TAttribute : IAttribute
        {
            return card is not null && card.Attributes.OfType<TAttribute>().Any();
        }
    }
}