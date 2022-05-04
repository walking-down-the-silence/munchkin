using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record AskForHelpAction(Player AskedPlayer) :
        ActionBase(TurnActions.Combat.AskForHelp, "Ask Player For Help", string.Empty),
        ICombatAction;
}
