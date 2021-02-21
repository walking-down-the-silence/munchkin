using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public class DiscardHandOrLoose2LevelsActions : Enumeration
    {
        private DiscardHandOrLoose2LevelsActions(int id, string name) : base(id, name)
        {
        }

        public static DiscardHandOrLoose2LevelsActions DiscardHand => new DiscardHandOrLoose2LevelsActions(1, "Discard Hand");

        public static DiscardHandOrLoose2LevelsActions Loose2Levels => new DiscardHandOrLoose2LevelsActions(2, "Loose 2 Levels");
    }
}
