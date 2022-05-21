using Munchkin.Core.Contracts.Events;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the player rolled the dice when running away from monster.
    /// </summary>
    public record RunningAwayFromMonsterDiceRollEvent(string PlayerNickname, string MonsterCardId, int DiceRollResult) :
        EventBase(DateTimeOffset.UtcNow),
        IRunningAwayEvent;
}
