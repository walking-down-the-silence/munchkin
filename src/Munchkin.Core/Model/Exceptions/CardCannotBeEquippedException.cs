using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class CardCannotBeEquippedException : Exception
    {
        public CardCannotBeEquippedException(string message) : base(message)
        {
        }
    }
}
