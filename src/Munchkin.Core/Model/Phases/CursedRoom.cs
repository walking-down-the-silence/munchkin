using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Treasures.OneShot;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record CursedRoom(
        Table Table,
        Player CurrentPlayer,
        CurseCard Card
    )
    : StateBase<CursedRoom>(Table, ImmutableList<Attribute>.Empty);

    public static class CursedRoomExtensions
    {
        public static IState From(Table table, Player currentPlayer, CurseCard curse)
        {
            // NOTE: If drawn face-up during the Kick Open The Door phase, Curse cards
            // apply to the person who drew them.
            return new CursedRoom(table, currentPlayer, curse);
        }

        public static IState ResolveWithWishingRing(this CursedRoom state, WishingRing card)
        {
            // NOTE: remove from player's cards and add it to the temporary pile,
            // before the step is resolved completely
            state.Table.Players.Current.Discard(card);
            return EmptyRoomExtensions.From(state.Table, state.CurrentPlayer);
        }

        public static IState TakeBadStuff(this CursedRoom state)
        {
            // TODO: pass the current player implicitly
            state.Card.BadStuff(state.Table);
            return state.CurrentPlayer.IsDead
                ? DeathExtensions.From(state.Table, state.CurrentPlayer)
                : EmptyRoomExtensions.From(state.Table, state.CurrentPlayer);
        }
    }
}