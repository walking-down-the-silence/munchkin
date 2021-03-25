using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Stages;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    public class PlayerOfferHelpAction : MultiShotAction
    {
        private readonly CombatRoomStep _combatRoom;
        private readonly Player _sourcePlayer;

        public PlayerOfferHelpAction(CombatRoomStep combatRoom, Player sourcePlayer) : base(int.MaxValue, "Offer Help", "")
        {
            _combatRoom = combatRoom ?? throw new ArgumentNullException(nameof(combatRoom));
            _sourcePlayer = sourcePlayer ?? throw new ArgumentNullException(nameof(sourcePlayer));
        }

        public override bool CanExecute(Table table)
        {
            return _combatRoom.HelpingPlayer == null
                && _combatRoom.FightingPlayer != _sourcePlayer;
        }

        public override async Task<Table> ExecuteAsync(Table table)
        {
            table = await base.ExecuteAsync(table);

            var helpDecision = await new PlayerDecideOnHelpRequest(table, _combatRoom.FightingPlayer).SendAsync(table);

            if (helpDecision == YesNoActions.Yes)
            {
                _combatRoom.HelpPlayerInCombat(_sourcePlayer);
            }

            return table;
        }
    }
}