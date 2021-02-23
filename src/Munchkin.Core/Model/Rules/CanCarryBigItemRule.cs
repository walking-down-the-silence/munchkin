using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Linq;

namespace Munchkin.Core.Model.Rules
{
    public class CanCarryBigItemRule : IRule<Table>
    {
        public bool Satisfies(Table state)
        {
            return state.Players.Current.Equipped.Any(card => card.Attributes.OfType<CarryAnyAmountOfBigItemsAttribute>().Any())
                && state.Players.Current.Equipped.OfType<ItemCard>().All(item => item.ItemSize != EItemSize.Big);
        }
    }
}
