using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class CardCannotBePlayedException : Exception
    {
        public CardCannotBePlayedException(string message) : base(message)
        {
        }
    }
}
