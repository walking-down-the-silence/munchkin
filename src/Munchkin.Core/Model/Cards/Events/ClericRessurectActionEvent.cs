using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record ClericRessurectActionEvent(string PlayerNickname, string DiscardCardId, string RessurectedCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;
}