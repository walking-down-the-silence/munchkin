using Munchkin.Core.Contracts.Actions;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class RollTheDiceAction : ActionBase, IRunningAwayAction
    {
        public RollTheDiceAction(Player player) :
            base(TurnActions.RunAway.RollTheDice, "Roll The Dice")
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public Player Player { get; }
    }
}