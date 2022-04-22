using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class TableExtensions
    {
        public static async Task<TableVM> ToVM(this ITableGrain table)
        {
            var players = await table.GetPlayersAsync();
            var expansionSelections = await table.GetIncludedExpansionsAsync();

            return new TableVM
            {
                Players = players.Select(x => x.ToVM()).ToArray(),
                ExpansionSelections = expansionSelections.Select(x => x.ToVM()).ToArray()
            };
        }
    }
}
