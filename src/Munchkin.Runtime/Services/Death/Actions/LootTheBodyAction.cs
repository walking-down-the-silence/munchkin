using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class LootTheBodyAction : ActionBase, IDeathAction
    {
        public LootTheBodyAction(Player player, Card card) :
            base(TurnActions.Death.LootTheBody, "Loot The Body")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }

        public Player Player { get; }
        public Card Card { get; }
    }
}
