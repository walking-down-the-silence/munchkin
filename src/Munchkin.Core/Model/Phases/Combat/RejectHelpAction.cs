using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record RejectHelpAction() : 
        ActionBase(TurnActions.AskingForHelp.RejectHelp, "Reject Help Request", string.Empty),
        ICombatAction;
}
