using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the player has taken the bad stuff from the curse.
    /// </summary>
    public record PlayerCurseBadStuffTakenEvent(string PlayerNickname, string CurseCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICurseEvent;
}