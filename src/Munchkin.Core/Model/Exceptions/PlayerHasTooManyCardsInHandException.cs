using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class PlayerHasTooManyCardsInHandException : Exception
    {
        public PlayerHasTooManyCardsInHandException() :
            base("Player has more than 5 cards in hand at the end of the turn.")
        {
        }
    }
}
