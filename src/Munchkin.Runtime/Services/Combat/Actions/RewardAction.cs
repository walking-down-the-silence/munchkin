using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record RewardAction(Table Table) :
        ActionBase(TurnActions.Combat.Reward, "Reward The Player", string.Empty),
        ICombatAction;
}
