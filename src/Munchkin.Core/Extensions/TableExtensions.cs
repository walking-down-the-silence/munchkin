using Munchkin.Core.Model;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Extensions
{
    public static class TableExtensions
    {
        public static async Task<Table> WaitForAllPlayers(this Table table)
        {
            // NOTE: map each player to their own Task Completion Source, so that they can end combat
            var playerResponses = table.Players
                .Select(player => new GameWaitForPlayerRequest(table, player))
                .Select(request => table.RequestSink.Send(request));

            // NOTE: select and wait for all players to end combat
            await Task.WhenAll(playerResponses);

            return table;
        }

        /// <summary>
        /// Gets if any of the players has won the game.
        /// </summary>
        public static bool IsGameOver(this Table table)
        {
            return table.Players.Any(x => x.Level >= table.WinningLevel);
        }
    }
}
