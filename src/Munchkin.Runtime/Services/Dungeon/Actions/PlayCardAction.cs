using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record PlayCardAction(Table Table, Player Player, Card Card) :
        ActionBase(TurnActions.Dungeon.PlayCard, "Play A Card", string.Empty),
        IDungeonAction;
}