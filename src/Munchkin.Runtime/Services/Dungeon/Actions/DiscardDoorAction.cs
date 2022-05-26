using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Actions;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class DiscardDoorAction : ActionBase, ICharityAction
    {
        public DiscardDoorAction(Player player, DoorsCard card) :
            base(TurnActions.Player.DiscardCard, "Discard The Door card")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }

        public Player Player { get; }
        public DoorsCard Card { get; }
    }
}
