using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    public class CanCarryBigItemRule : IRule<Table>
    {
        public bool Satisfies(Table table)
        {
            var currentPlayer = table.Players.Current;
            var bigItemsCarried = currentPlayer.Equipped
                .Concat(currentPlayer.Backpack)
                .Where(x => x.HasAttribute<ItemSizeAttribute>())
                .Where(x => x.GetAttribute<ItemSizeAttribute>().ItemSize == EItemSize.Big)
                .Count();

            return bigItemsCarried < currentPlayer.GetMaximumBigItemsCarried();
        }
    }
}
