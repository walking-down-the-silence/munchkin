using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record ClericRessurectActionEvent(string PlayerNickname, string DiscardCardId, string RessurectedCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;
}