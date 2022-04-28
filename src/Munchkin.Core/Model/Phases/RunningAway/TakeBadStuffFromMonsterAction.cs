namespace Munchkin.Core.Model.Phases
{
    public record TakeBadStuffFromMonsterAction(
        Player Player) : IRunningAwayAction;
}