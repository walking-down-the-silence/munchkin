using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public static class PlayerTurn
    {
        public static Task Start(Table table)
        {
            // TODO: pass a valid reference for Mediator
            return NextTurn(ImmutableStack<Table>.Empty, table);
        }

        private static async Task NextTurn(ImmutableStack<Table> history, Table table)
        {
            if (!table.IsGameWon)
            {
                // TODO: handle a case where some cards need to access current stage instance
                table.Dungeon.KickOpenTheDoor();

                while (await table.Dungeon.MoveToNextStage(table))
                {
                }

                history.Push(table);
                table.Players.Next();

                await NextTurn(history, table);
            }
        }
    }
}
