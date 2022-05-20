using Munchkin.Core.Contracts.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record RollTheDiceAction(Player Player) :
        ActionBase(TurnActions.RunAway.RollTheDice, "Roll The Dice", string.Empty),
        IRunningAwayAction;
}