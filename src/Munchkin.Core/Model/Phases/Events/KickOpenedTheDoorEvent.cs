using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record KickOpenedTheDoorEvent(string PlayerNickname, string DoorCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IEnteredStateEvent;
}