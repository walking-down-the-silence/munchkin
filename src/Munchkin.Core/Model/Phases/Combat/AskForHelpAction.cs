namespace Munchkin.Core.Model.Phases
{
    public record AskForHelpAction(
        Player AskedPlayer) : ICombatAction;
}
