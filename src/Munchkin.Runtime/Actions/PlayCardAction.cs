using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model
{
    public record PlayCardAction(Player Player, Card Card) :
        ActionBase(TurnActions.Dungeon.PlayCard, "Play A Card", string.Empty),
        IAction;
}
