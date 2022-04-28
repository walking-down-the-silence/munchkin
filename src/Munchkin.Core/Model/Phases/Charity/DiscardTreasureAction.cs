﻿using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record DiscardTreasureAction(
        Player Player,
        TreasureCard Card) : ICharityAction;
}
