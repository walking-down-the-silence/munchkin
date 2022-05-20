using System;

namespace Munchkin.Core.Contracts.Exceptions
{
    public class CardCannotBeEquippedException : Exception
    {
        public CardCannotBeEquippedException(string message) : base(message)
        {
        }
    }
}
