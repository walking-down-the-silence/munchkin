using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class PlayerCannotPerformActionException : Exception
    {
        public PlayerCannotPerformActionException(string message) : base(message)
        {
        }
    }
}
