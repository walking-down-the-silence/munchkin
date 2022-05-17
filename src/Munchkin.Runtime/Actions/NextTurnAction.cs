using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model
{
    public record NextTurnAction(Table Table) :
        ActionBase(TurnActions.NextTurn, "Next Turn", string.Empty),
        IAction;
}
