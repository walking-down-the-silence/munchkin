using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Runtime.Services
{
    public class CardService
    {
        public static string GetUniqueId(Card card) => $"card_{card.GetHashCode()}";
    }
}
