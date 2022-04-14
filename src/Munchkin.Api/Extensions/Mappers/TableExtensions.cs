using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions.Tables;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class TableExtensions
    {
        public static async Task<TableVM> ToVM(this ITable table)
        {
            var players = await table.GetPlayers();
            var expansionSelections = await table.GetExpansionSelections();

            return new TableVM
            {
                Players = players.Select(x => x.ToVM()).ToArray(),
                ExpansionSelections = expansionSelections.Select(x => x.ToVM()).ToArray()
            };
        }
    }
}
