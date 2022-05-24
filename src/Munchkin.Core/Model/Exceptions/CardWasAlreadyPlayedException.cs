using System;

namespace Munchkin.Core.Model.Exceptions
{
    public class CardWasAlreadyPlayedException : Exception
    {
        public CardWasAlreadyPlayedException() :
            base("The card cannot be played, beacause it has already been played and is discarded.")
        {
        }
    }
}
