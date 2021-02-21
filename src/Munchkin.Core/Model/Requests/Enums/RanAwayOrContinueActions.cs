using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public class RanAwayOrContinueActions : Enumeration
    {
        private RanAwayOrContinueActions(int id, string name) : base(id, name)
        {
        }

        public static RanAwayOrContinueActions RunAway => new RanAwayOrContinueActions(1, "Run Away");

        public static RanAwayOrContinueActions Continue => new RanAwayOrContinueActions(2, "Continue");
    }
}
