using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class CurseCannotBeCancelledException : Exception
    {
        public CurseCannotBeCancelledException() :
            base("The chosen card does not have the ability to cancel curses.")
        {
        }
    }
}
