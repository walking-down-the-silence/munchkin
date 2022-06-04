using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record CombatMosterRemovedEvent(string PlayerNickname, string MonsterCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICombatEvent,
        IEnteredStateEvent;
}