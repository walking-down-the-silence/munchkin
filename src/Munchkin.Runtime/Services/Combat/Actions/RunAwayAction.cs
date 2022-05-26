using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public sealed class RunAwayAction : ActionBase, ICombatAction
    {
        public RunAwayAction() :
            base(TurnActions.Combat.RunAway, "Run Away From Monster")
        {
        }
    }
}
