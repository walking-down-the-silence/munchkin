using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record ThiefTheftActionEvent(string PlayerNickname, string DiscardCardId) :
        EventBase(DateTimeOffset.UtcNow),
        IClassActionEvent;
}