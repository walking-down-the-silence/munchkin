using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Cards.Treasures.OneShot;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// TODO: handle a case when the player does not have a wishing ring, but can play other card to obtain one.
    /// TODO: handle a case of highlighting the cards that can resolve a curse (not only Wishing Ring can resolve a curse).
    /// </summary>
    public static class CursingThePlayer
    {
        public static IState ResolveWithWishingRing(this Cursed state, WishingRing card)
        {
            // NOTE: remove from player's cards and add it to the temporary pile, before the step is resolved completely
            state.Table.Players.Current.Discard(card);
            return state.PreviousState;
        }

        public static IState TakeBadStuff(this Cursed state)
        {
            // TODO: pass the current player implicitly
            state.Card.BadStuff(state.Table);
            return state.PreviousState;
        }
    }
}