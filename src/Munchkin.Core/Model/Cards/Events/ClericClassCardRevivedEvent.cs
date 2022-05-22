using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public record ClericClassCardRevivedEvent(string PlayerNickname, string RevivedCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;

    public record ClericClassBonus3AgainstUndeadEvent(string PlayerNickname, string DiscardedCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;

    public interface IClassActionEvent : IEvent
    {
    }
}