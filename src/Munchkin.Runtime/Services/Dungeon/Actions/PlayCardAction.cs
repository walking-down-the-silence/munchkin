using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class PlayCardAction : ActionBase, IDungeonAction
    {
        public PlayCardAction(Player player, Card card) :
            base(TurnActions.Dungeon.PlayCard, "Play A card")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }

        public Player Player { get; }
        public Card Card { get; }
    }
}