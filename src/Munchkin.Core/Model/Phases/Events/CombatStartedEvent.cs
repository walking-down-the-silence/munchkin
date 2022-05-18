namespace Munchkin.Core.Model.Phases.Events
{
    public record CombatStartedEvent(string PlayerNickname, string MonsterCardId);
}