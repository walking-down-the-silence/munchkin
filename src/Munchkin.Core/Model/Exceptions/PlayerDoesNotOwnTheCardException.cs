using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class PlayerDoesNotOwnTheCardException : Exception
    {
        public PlayerDoesNotOwnTheCardException() :
            base("Player does not own the card that they were trying to play.")
        {
        }
    }
}
