using Munchkin.Core.Contracts;
using Munchkin.Core.Model;

namespace Munchkin.Core.Cards.Effects
{
    public class DiscardCardsFromHandEffect : IEffect<Table>
    {
        public Table Apply(Table state)
        {
            state.Players.Current.DiscardHand();
            return state;
        }
    }
}
