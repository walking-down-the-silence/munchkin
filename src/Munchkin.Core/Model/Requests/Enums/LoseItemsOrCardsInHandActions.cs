using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests.Enums
{
    public sealed record LoseItemsOrCardsInHandActions : Enumeration
    {
        private LoseItemsOrCardsInHandActions(int code, string name) : base(code, name)
        {
        }

        public static LoseItemsOrCardsInHandActions LoseItems => new(1, "Lose Items");

        public static LoseItemsOrCardsInHandActions LoseCardsInHand => new(2, "Lose Cards In Hand");
    }
}
