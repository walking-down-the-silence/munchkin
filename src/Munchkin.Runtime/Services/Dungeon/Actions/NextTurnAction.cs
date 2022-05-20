using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model
{
    public record NextTurnAction(Table Table) :
        ActionBase(TurnActions.Table.NextTurn, "Next Turn", string.Empty),
        IAction;
}
