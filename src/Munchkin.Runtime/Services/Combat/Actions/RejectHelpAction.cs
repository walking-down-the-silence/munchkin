using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public sealed class RejectHelpAction : ActionBase, ICombatAction
    {
        public RejectHelpAction() :
            base(TurnActions.AskingForHelp.RejectHelp, "Reject Help Request")
        {
        }
    }
}
