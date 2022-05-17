﻿using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record DiscardDoorAction(Player Player, DoorsCard Card) :
        ActionBase(TurnActions.Player.DiscardCard, "Discard The Door Card", string.Empty),
        ICharityAction;
}
