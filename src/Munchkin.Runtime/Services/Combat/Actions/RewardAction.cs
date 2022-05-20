using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record RewardAction(Table Table) :
        ActionBase(TurnActions.Combat.Reward, "Reward The Player", string.Empty),
        ICombatAction;
}
