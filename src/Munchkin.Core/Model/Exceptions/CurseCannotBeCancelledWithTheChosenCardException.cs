using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class CurseCannotBeCancelledWithTheChosenCardException : Exception
    {
        public CurseCannotBeCancelledWithTheChosenCardException() :
            base("The chosen card does not have the ability to cancel curses.")
        {
        }
    }
}
