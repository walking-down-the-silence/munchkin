using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Services;

namespace Munchkin.Core.Model.Effects
{
    public class DiscardCardsFromHandEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            PlayerAvatar.DiscardHand(state, state.Players.Current);
            return state;
        }
    }
}
