using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Stages;

namespace Munchkin.Core.Model.Effects
{
    public class KillPlayersAvatarEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            PlayerAvatar.Kill(state, state.Players.Current);
            return state;
        }
    }
}
