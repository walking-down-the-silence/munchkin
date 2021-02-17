using Munchkin.Console;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public static class PlayerTurn
    {
        public static Task Start(IEnumerable<Player> players, int winningLevel)
        {
            return NextTurn(ImmutableStack<Table>.Empty, Table.Setup(players, winningLevel));
        }

        private static async Task NextTurn(ImmutableStack<Table> history, Table table)
        {
            if (!table.IsGameWon)
            {
                var dungeonFlow = new DungeonFlowFactory().Create().Build();

                dungeonFlow.Invoke(table.Dungeon);
                table.Players.Next();

                await NextTurn(history, table);
            }
        }
    }
}
