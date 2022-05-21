using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when a player was asked to help in combat.
    /// </summary>
    public record AskingForHelpPlayerEvent(string AskedPlayerNickname) :
        EventBase(DateTimeOffset.UtcNow),
        IAskingForHelpEvent;
}
