using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System;

namespace Munchkin.Runtime.Actions
{
    public sealed class GiveAwayAction : ActionBase, ICharityAction
    {
        public GiveAwayAction(Player player, TreasureCard card) :
            base(TurnActions.Charity.GiveAway, "Give Away The Card")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }

        public Player Player { get; }
        public TreasureCard Card { get; }
    }
}
