using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record KickOpenedTheDoorEvent(string PlayerNickname, string DoorCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IEnteredStateEvent;
}