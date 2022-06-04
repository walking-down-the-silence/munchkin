using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when one player gave the card to another player as charity.
    /// </summary>
    public record CharityGivenAwayEvent(string GiverNickname, string TakerNickname, string CardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICharityEvent,
        IEnteredStateEvent;
}
