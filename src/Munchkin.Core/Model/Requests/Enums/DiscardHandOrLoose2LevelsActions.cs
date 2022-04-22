using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests.Enums
{
    public sealed record DiscardHandOrLoose2LevelsActions : Enumeration
    {
        private DiscardHandOrLoose2LevelsActions(int code, string name) : base(code, name)
        {
        }

        public static DiscardHandOrLoose2LevelsActions DiscardHand => new(1, "Discard Hand");

        public static DiscardHandOrLoose2LevelsActions Loose2Levels => new(2, "Loose 2 Levels");
    }
}
