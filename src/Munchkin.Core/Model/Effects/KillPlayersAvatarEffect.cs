using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Effects
{
    public class KillPlayersAvatarEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            state.Players.Current.Kill(state);
            return state;
        }
    }
}
