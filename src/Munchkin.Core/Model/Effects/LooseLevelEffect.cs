using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Effects
{
    public class LooseLevelEffect : IEffect<Table>
    {
        private readonly int _levelsToLoose;

        public LooseLevelEffect(int levelsToLoose)
        {
            _levelsToLoose = levelsToLoose;
        }

        public Table Apply(Table state)
        {
            // TODO: before applying, leave a change for player to discard a curse
            for (int i = 0; i < _levelsToLoose; i++)
            {
                state.Players.Current.LevelDown();
            }
            return state;
        }
    }
}
