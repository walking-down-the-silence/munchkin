using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model
{
    public record PlayCardAction(Card Card) : IAction;
}
