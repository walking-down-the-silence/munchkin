using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed record PlayerDiceRolledEvent(string PlayerNickname, int DiceRoll) :
        EventBase(DateTimeOffset.UtcNow);
}
