using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class InvalidCardUsedException : Exception
    {
        public InvalidCardUsedException(string message) : base(message)
        {
        }
    }
}
