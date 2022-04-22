using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Treasures.OneShot;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Curse(
        Table Table,
        Player TargetPlayer,
        CurseCard Card,
        ImmutableList<Card> TemporaryPile,
        CombatRoom PreviousState
    )
    : StateBase<Curse>(Table, ImmutableList<Attribute>.Empty);

    public static class CurseExtensions
    {
        public static IState From(Table table, Player targetPlayer, CurseCard curse, CombatRoom previousState)
        {
            // NOTE: If drawn face-up during the Kick Open The Door phase, Curse cards
            // apply to the person who drew them.
            return new Curse(
                table,
                targetPlayer,
                curse,
                ImmutableList<Card>.Empty,
                previousState);
        }

        public static IState ResolveWithWishingRing(this Curse state, WishingRing card)
        {
            // NOTE: remove from player's cards and add it to the temporary pile, before the step is resolved completely
            state.Table.Players.Current.Discard(card);
            return state.PreviousState;
        }

        public static IState AcceptBadStuff(this Curse state)
        {
            // TODO: pass the current player implicitly
            state.Card.BadStuff(state.Table);
            return state.PreviousState;
        }
    }
}