using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public static class PlayerTurn
    {
        public static Task Start(IEnumerable<Player> players, int winningLevel)
        {
            // TODO: pass a valid reference for Mediator
            return NextTurn(ImmutableStack<Table>.Empty, Table.Setup(null, players, null, null, winningLevel));
        }

        private static async Task NextTurn(ImmutableStack<Table> history, Table table)
        {
            if (!table.IsGameWon)
            {
                //var dungeonFlow = new DungeonFlowFactory().Create().Build();

                //dungeonFlow.Invoke(table.Dungeon);
                table.Players.Next();

                await NextTurn(history, table);
            }
        }
    }
}
