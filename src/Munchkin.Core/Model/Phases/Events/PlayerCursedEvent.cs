using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record PlayerCursedEvent(string PlayerNickname, string CurseCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICurseEvent;

    public record PlayerCurseResolved(string PlayerNickname, string CurseCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICurseEvent;

    public record PlayerCurseBadStuffTaken(string PlayerNickname, string CurseCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICurseEvent;

    public interface ICurseEvent
    {
        DateTimeOffset CreatedDate { get; }
    }
}