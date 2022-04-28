using Munchkin.Api.ViewModels;
using Munchkin.Core.Model;
using System.Linq;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class TableExtensions
    {
        public static TableVM ToVM(this Table table)
        {
            return new TableVM
            {
                Players = table.Players.Select(x => x.ToVM()).ToArray(),
                ExpansionSelections = table.IncludedExpansions.Select(x => x.ToVM()).ToArray()
            };
        }
    }
}
