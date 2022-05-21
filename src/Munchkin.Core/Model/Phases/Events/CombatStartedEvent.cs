using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the combat started in the dungeon.
    /// </summary>
    public record CombatStartedEvent(string PlayerNickname, string MonsterCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICombatEvent,
        IEnteredStateEvent;
}