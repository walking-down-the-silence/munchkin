using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Turn(
        Table Table
    )
    : StateBase<Turn>(Table, ImmutableList<Attribute>.Empty);

    public static class TurnExtensions
    {
        public static IState From(Table table)
        {
            return new Turn(table);
        }
    }
}
