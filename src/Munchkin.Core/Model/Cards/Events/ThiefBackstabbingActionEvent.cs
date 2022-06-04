using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record ThiefBackstabbingActionEvent(string PlayerNickname, string DiscardCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;
}