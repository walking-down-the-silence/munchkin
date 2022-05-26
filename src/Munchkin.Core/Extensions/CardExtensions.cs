using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class CardExtensions
    {
        public static IEnumerable<Card> ExceptType<TExclude>(this IEnumerable<Card> cards)
        {
            return cards.Where(card => card is not TExclude);
        }

        public static void DiscardAll(this IEnumerable<Card> cards, Table table)
        {
            foreach (var card in cards)
            {
                card.Discard(table);
            }
        }
    }
}