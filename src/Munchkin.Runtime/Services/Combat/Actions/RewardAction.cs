using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public sealed class RewardAction : ActionBase, ICombatAction
    {
        public RewardAction() :
            base(TurnActions.Combat.Reward, "Reward The Player")
        {
        }
    }
}
