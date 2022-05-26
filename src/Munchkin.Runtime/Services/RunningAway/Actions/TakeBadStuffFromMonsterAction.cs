using Munchkin.Core.Contracts.Actions;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class TakeBadStuffFromMonsterAction : ActionBase, IRunningAwayAction
    {
        public TakeBadStuffFromMonsterAction(Player player) :
            base(TurnActions.RunAway.TakeBadStuff, "TakenBy Bad Stuff")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public Player Player { get; }
    }
}