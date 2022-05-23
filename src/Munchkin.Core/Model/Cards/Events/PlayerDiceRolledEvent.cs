using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record PlayerDiceRolledEvent(string PlayerNickname, int DiceRoll) :
        EventBase(DateTimeOffset.UtcNow);
}
