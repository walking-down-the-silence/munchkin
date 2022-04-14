using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public sealed record RanAwayOrContinueActions : Enumeration
    {
        private RanAwayOrContinueActions(int code, string name) : base(code, name)
        {
        }

        public static RanAwayOrContinueActions RunAway => new RanAwayOrContinueActions(1, "Run Away");

        public static RanAwayOrContinueActions Continue => new RanAwayOrContinueActions(2, "Continue");
    }
}
