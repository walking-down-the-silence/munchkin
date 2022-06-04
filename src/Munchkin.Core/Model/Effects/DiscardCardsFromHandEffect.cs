using Munchkin.Core.Contracts;
using Munchkin.Core.Extensions;

namespace Munchkin.Core.Model.Effects
{
    public class DiscardCardsFromHandEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            state.DiscardPlayersHand(state.Players.Current);
            return state;
        }
    }
}
