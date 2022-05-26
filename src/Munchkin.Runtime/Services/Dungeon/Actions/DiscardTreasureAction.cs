using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Actions;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class DiscardTreasureAction : ActionBase, ICharityAction
    {
        public DiscardTreasureAction(Player player, TreasureCard card) :
            base(TurnActions.Player.DiscardCard, "Discard The Treasure Card")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }

        public Player Player { get; }
        public TreasureCard Card { get; }
    }
}
