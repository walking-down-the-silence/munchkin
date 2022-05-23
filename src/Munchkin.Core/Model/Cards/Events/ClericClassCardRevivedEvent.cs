using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record ClericClassCardRevivedEvent(string PlayerNickname, string RevivedCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;
}