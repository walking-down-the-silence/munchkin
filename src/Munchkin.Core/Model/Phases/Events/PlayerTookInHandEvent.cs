using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record PlayerTookInHandEvent(string PlayerNickname, string CardId) :
        EventBase(DateTimeOffset.UtcNow);
}