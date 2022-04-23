using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Cursed(
        Table Table,
        Player TargetPlayer,
        CurseCard Card,
        ImmutableList<Card> TemporaryPile,
        CombatRoom PreviousState
    ) : StateBase<Cursed>(Table, TargetPlayer, ImmutableList<Attribute>.Empty)
    {
        public static IState From(Table table, Player targetPlayer, CurseCard curse, CombatRoom previousState)
        {
            // NOTE: If drawn face-up during the Kick Open The Door phase, Curse cards
            // apply to the person who drew them.
            return new Cursed(
                table,
                targetPlayer,
                curse,
                ImmutableList<Card>.Empty,
                previousState);
        }
    }
}