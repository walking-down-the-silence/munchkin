using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record AskingForHelpAcceptedEvent(string AskedPlayerNickname) :
        EventBase(DateTimeOffset.UtcNow),
        IAskingForHelpEvent;
}
