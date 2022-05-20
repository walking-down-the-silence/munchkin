using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record RejectHelpAction() : 
        ActionBase(TurnActions.AskingForHelp.RejectHelp, "Reject Help Request", string.Empty),
        ICombatAction;
}
