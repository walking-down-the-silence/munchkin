using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;

namespace Munchkin.Core.Model.Effects
{
    public class KillPlayersAvatarEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            state.KillPlayer(state.Players.Current);
            return state;
        }
    }
}
