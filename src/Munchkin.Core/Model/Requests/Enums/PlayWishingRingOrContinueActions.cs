using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public class PlayWishingRingOrContinueActions : Enumeration
    {
        private PlayWishingRingOrContinueActions(int id, string name) : base(id, name)
        {
        }

        public static PlayWishingRingOrContinueActions PlayWishingRing => new PlayWishingRingOrContinueActions(1, "Play Wishing Ring");

        public static PlayWishingRingOrContinueActions Continue => new PlayWishingRingOrContinueActions(2, "Continue");
    }
}
