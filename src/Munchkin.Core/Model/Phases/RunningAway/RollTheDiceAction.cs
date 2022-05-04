using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Model.Phases
{
    public record RollTheDiceAction(Player Player) :
        ActionBase(TurnActions.Combat.RollTheDice, "Roll The Dice", string.Empty),
        IRunningAwayAction;
}