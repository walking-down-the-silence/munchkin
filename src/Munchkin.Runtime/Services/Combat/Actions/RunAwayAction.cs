using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record RunAwayAction() :
        ActionBase(TurnActions.Combat.RunAway, "Run Away From Monster", string.Empty),
        ICombatAction;
}
