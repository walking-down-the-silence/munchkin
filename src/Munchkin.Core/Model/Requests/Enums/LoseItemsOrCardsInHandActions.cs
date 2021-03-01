using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests.Enums
{
    public class LoseItemsOrCardsInHandActions : Enumeration
    {
        private LoseItemsOrCardsInHandActions(int id, string name) : base(id, name)
        {
        }

        public static LoseItemsOrCardsInHandActions LoseItems => new(1, "Lose Items");

        public static LoseItemsOrCardsInHandActions LoseCardsInHand => new(2, "Lose Cards In Hand");
    }
}
