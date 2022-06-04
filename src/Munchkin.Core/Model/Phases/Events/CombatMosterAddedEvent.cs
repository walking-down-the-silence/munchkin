﻿using Munchkin.Core.Contracts;
using System;

namespace Munchkin.Core.Model.Phases.Events
{
    /// <summary>
    /// Defines an event when the combat started in the dungeon.
    /// </summary>
    public record CombatMosterAddedEvent(string PlayerNickname, string MonsterCardId) :
        EventBase(DateTimeOffset.UtcNow),
        ICombatEvent,
        IEnteredStateEvent;
}