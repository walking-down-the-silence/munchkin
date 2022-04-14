using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public sealed record PlayWishingRingOrContinueActions : Enumeration
    {
        private PlayWishingRingOrContinueActions(int code, string name) : base(code, name)
        {
        }

        public static PlayWishingRingOrContinueActions PlayWishingRing => new PlayWishingRingOrContinueActions(1, "Play Wishing Ring");

        public static PlayWishingRingOrContinueActions Continue => new PlayWishingRingOrContinueActions(2, "Continue");
    }
}
