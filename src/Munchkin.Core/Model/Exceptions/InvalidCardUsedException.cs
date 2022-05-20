using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class InvalidCardUsedException : Exception
    {
        public InvalidCardUsedException(string message) : base(message)
        {
        }
    }

    public class PlayerHasTooManyCardsInHandException : Exception
    {
        public PlayerHasTooManyCardsInHandException() : base("Player has more than 5 cards in hand at the end of the turn.")
        {
        }
    }
}
