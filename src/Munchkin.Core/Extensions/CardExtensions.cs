using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
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

        public static FaceDownCard AsFaceDown(this Card card)
        {
            return new FaceDownCard(card);
        }

        public static IEnumerable<FaceDownCard> AsFaceDown(this IEnumerable<Card> cards)
        {
            return cards.Select(card => card.AsFaceDown());
        }

        public static Card AsFaceUp(this FaceDownCard faceDownCard)
        {
            return faceDownCard.TurnFaceUp();
        }

        public static IEnumerable<Card> AsFaceUp(this IEnumerable<FaceDownCard> faceDownCards)
        {
            return faceDownCards.Select(faceDownCard => faceDownCard.AsFaceUp());
        }

        public static T AsFaceUp<T>(this FaceDownCard faceDownCard) where T : Card
        {
            return faceDownCard.TurnFaceUp() as T;
        }

        public static IEnumerable<T> AsFaceUp<T>(this IEnumerable<FaceDownCard> faceDownCards) where T : Card
        {
            return faceDownCards.Select(faceDownCard => faceDownCard.AsFaceUp<T>());
        }
    }
}