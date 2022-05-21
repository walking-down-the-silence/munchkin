using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when a player rejected the help request.
    /// </summary>
    public record AskingForHelpRejectedEvent(string AskedPlayerNickname) :
        EventBase(DateTimeOffset.UtcNow),
        IAskingForHelpEvent;
}
