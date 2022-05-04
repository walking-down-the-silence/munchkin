using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record AcceptHelpAction(Player AskedPlayer) :
        ActionBase(TurnActions.Combat.AcceptHelp, "Accept Help Request", string.Empty),
        ICombatAction;
}
