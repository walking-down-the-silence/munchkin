using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model
{
    public record NextTurnAction() :
        ActionBase(TurnActions.NextTurn, "Next Turn", string.Empty),
        IAction;
}
