using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when a player accepted the help request.
    /// </summary>
    public record AskingForHelpAcceptedEvent(string AskedPlayerNickname) :
        EventBase(DateTimeOffset.UtcNow),
        IAskingForHelpEvent;
}
