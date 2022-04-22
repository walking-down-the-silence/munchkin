using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Asks other player to help in battle
    /// </summary>
    public class PlayerAskForHelpAction : MultiShotAction
    {
        private readonly CombatRoomStep _combatRoom;
        private readonly List<Player> _rejectedPlayers = new();

        public PlayerAskForHelpAction(CombatRoomStep combatRoom) : base(int.MaxValue, "Ask For Help", "")
        {
            _combatRoom = combatRoom ?? throw new ArgumentNullException(nameof(combatRoom));
            _rejectedPlayers = new List<Player> { combatRoom.FightingPlayer };
        }

        public override bool CanExecute(Table table)
        {
            // NOTE: check if sufficient executions left and if there are any players left to ask
            return ExecutionsLeft > 0
                && table.Players.Count > _rejectedPlayers.Count
                && _combatRoom.HelpingPlayer == null;
        }

        public override async Task<Table> ExecuteAsync(Table table)
        {
            table = await base.ExecuteAsync(table);

            var playerOptions = table.Players.Except(_rejectedPlayers).ToList();
            var selectedPlayer = await new PlayerSelectSinglePlayerRequest(table, _combatRoom.FightingPlayer, playerOptions).SendAsync(table);

            var helpDecision = await new PlayerDecideOnHelpRequest(table, selectedPlayer).SendAsync(table);

            if (helpDecision == YesNoActions.Yes)
            {
                _combatRoom.HelpPlayerInCombat(selectedPlayer);
            }
            else
            {
                _rejectedPlayers.Add(selectedPlayer);
            }

            return table;
        }
    }
}