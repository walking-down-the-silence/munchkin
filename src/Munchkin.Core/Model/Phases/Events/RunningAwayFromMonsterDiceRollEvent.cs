using System;

namespace Munchkin.Core.Model.Phases.Events
{
    public record RunningAwayFromMonsterDiceRollEvent(string PlayerNickname, string MonsterCardId, int DiceRollResult) :
        EventBase(DateTimeOffset.UtcNow),
        IRunningAwayEvent;
}
