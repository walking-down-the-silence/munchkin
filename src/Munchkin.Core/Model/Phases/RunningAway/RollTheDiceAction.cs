namespace Munchkin.Core.Model.Phases
{
    public record RollTheDiceAction(
        Player Player) : IRunningAwayAction;
}