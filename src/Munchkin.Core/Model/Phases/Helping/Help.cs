using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record Help(
        Table Table,
        ImmutableList<Player> PlayersToAsk,
        Player FightingPlayer,
        Player HelpingPlayer,
        CombatRoom PreviousState
    ) : StateBase<Help>(Table, FightingPlayer, ImmutableList<Attribute>.Empty)
    {
        public static IState From(Table table, CombatRoom previousState)
        {
            var playersToAsk = ImmutableList.CreateRange(table.Players.Where(p => p != table.Players.Current));
            return new Help(
                table,
                playersToAsk,
                previousState.FightingPlayer,
                null,
                previousState);
        }
    }
}
