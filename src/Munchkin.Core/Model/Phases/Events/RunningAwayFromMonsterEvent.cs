using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the player decided to run away from monster.
    /// </summary>
    public record RunningAwayFromMonsterEvent(string PlayerNickname, string MonsterCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IRunningAwayEvent,
        IEnteredStateEvent;
}
