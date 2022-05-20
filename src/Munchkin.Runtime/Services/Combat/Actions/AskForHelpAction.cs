using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record AskForHelpAction(Player AskedPlayer) :
        ActionBase(TurnActions.AskingForHelp.AskForHelp, "Ask Player For Help", string.Empty),
        ICombatAction;
}
