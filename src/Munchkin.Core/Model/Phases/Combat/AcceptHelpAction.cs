using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record AcceptHelpAction(Player AskedPlayer) :
        ActionBase(TurnActions.AskingForHelp.AcceptHelp, "Accept Help Request", string.Empty),
        ICombatAction;
}
