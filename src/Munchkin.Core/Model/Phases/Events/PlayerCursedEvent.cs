using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the player was cursed.
    /// </summary>
    public record PlayerCursedEvent(string PlayerNickname, string CurseCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICurseEvent,
        IEnteredStateEvent;
}