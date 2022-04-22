using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Reward(
        Table Table
    )
    : StateBase<Reward>(Table, ImmutableList<Attribute>.Empty);

    public static class RewardExtensions
    {
        public static IState From(Table table)
        {
            return new Reward(table);
        }
    }
}
