namespace Munchkin.Core.Model.Phases
{
    public record AcceptHelpAction(
        Player AskedPlayer) : ICombatAction;
}
