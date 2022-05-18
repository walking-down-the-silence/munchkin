using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record RunningAwayFromMonsterEvent(string PlayerNickname, string MonsterCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IRunningAwayEvent;
}
