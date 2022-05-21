using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record PlayerTookInHandEvent(string PlayerNickname, string CardId) :
        EventBase(DateTimeOffset.UtcNow);
}