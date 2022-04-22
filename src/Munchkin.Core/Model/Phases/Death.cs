using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Death(
        Table Table,
        Player TargetPlayer
    )
    : StateBase<Death>(Table, ImmutableList<Attribute>.Empty);

    public static class DeathExtensions
    {
        public static IState From(Table table, Player targetPlayer)
        {
            return new Death(table, targetPlayer);
        }
    }
}
