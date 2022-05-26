using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class ResolveAction : ActionBase, ICurseAction
    {
        public ResolveAction(Card card) :
            base(TurnActions.Curse.Resolve, "Resolve The Curse")
        {
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }

        public Card Card { get; }
    }
}