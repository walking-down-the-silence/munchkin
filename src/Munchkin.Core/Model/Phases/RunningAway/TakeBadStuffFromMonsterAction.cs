using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromMonsterAction(Player Player) :
        ActionBase(TurnActions.Combat.TakeBadStuffFromMonster, "Take Bad Stuff", string.Empty),
        IRunningAwayAction;
}