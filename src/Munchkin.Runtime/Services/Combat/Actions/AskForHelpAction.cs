using Munchkin.Core.Contracts.Actions;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class AskForHelpAction : ActionBase, ICombatAction
    {
        public AskForHelpAction(Player askedPlayer) :
            base(TurnActions.AskingForHelp.AskForHelp, "Ask Player For Help")
        {
            AskedPlayer = askedPlayer ?? throw new ArgumentNullException(nameof(askedPlayer));
        }

        public Player AskedPlayer { get; }
    }
}
