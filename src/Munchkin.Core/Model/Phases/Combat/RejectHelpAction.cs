using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record RejectHelpAction() : 
        ActionBase(TurnActions.Combat.RejectHelp, "Reject Help Request", string.Empty),
        ICombatAction;
}
