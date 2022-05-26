using Munchkin.Core.Contracts.Actions;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class AcceptHelpAction : ActionBase, ICombatAction
    {
        public AcceptHelpAction(Player askedPlayer) :
            base(TurnActions.AskingForHelp.AcceptHelp, "Accept Help Request")
        {
            AskedPlayer = askedPlayer ?? throw new ArgumentNullException(nameof(askedPlayer));
        }

        public Player AskedPlayer { get; }
    }
}
