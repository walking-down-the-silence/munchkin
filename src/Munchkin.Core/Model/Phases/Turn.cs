using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Turn(
        Table Table,
        IState Current,
        ImmutableList<Attribute> Attributes)
    {
        public static Turn From(Table table)
        {
            // TODO: revive the current player if is dead
            table.Players.Next();
            var dungeon = new Dungeon(table, table.Players.Current);
            return new Turn(table, dungeon, ImmutableList<Attribute>.Empty);
        }
    }

    public static class TurnExtensions
    {
        public static Turn Execute(this Turn state, System.Func<IState, IState> func)
        {
            return state with { Current = func(state.Current) };
        }
    }
}
