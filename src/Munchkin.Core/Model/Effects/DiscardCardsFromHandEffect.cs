using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Effects
{
    public class DiscardCardsFromHandEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            state.Players.Current.DiscardHand(state);
            return state;
        }
    }
}
