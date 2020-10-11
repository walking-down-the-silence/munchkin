using Munchkin.Core.Cards.Traits;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using System.Linq;

namespace Munchkin.Core.Cards.Rules
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
