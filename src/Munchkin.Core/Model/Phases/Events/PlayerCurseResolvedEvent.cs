using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the player resolved the curse by playing a card.
    /// </summary>
    public record PlayerCurseResolvedEvent(string PlayerNickname, string CurseCardId, string ResolveCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICurseEvent;
}