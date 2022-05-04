using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record RunAwayAction() :
        ActionBase(TurnActions.Combat.RunAway, "Run Away From Monster", string.Empty),
        ICombatAction;
}
