using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RunAwayStep : HierarchialStep<Table>
    {
        private readonly Player _fightingPlayer;
        private readonly Player _helpingPlayer;
        private readonly IReadOnlyCollection<MonsterCard> _monsters;

        public RunAwayStep(
            Player fightingPlayer,
            Player helpingPlayer,
            IReadOnlyCollection<MonsterCard> monsters)
        {
            _fightingPlayer = fightingPlayer ?? throw new System.ArgumentNullException(nameof(fightingPlayer));
            _helpingPlayer = helpingPlayer ?? throw new System.ArgumentNullException(nameof(helpingPlayer));
            _monsters = monsters ?? throw new System.ArgumentNullException(nameof(monsters));
        }

        public override async Task<Table> Resolve(Table table)
        {
            table = await HandlePlayerDecisionToRunAway(table, _fightingPlayer);
            table = await HandlePlayerDecisionToRunAway(table, _helpingPlayer);
            var stage = new EndStep();
            return await stage.Resolve(table);
        }

        private async Task<Table> HandlePlayerDecisionToRunAway(Table table, Player player)
        {
            var playerRequest = new PlayerRanAwayRequest(player, table);
            var playerResponse = await table.RequestSink.Send(playerRequest);
            var playerAction = await playerResponse.Task;

            if (playerAction == RanAwayOrContinueActions.RunAway)
            {
                var diceRoll = Dice.Roll();

                if (diceRoll < 5)
                {
                    table = await TakeBadStuff(table, player);
                }
            }
            else
            {
                table = await TakeBadStuff(table, player);
            }

            return table;
        }

        private async Task<Table> TakeBadStuff(Table table, Player player)
        {
            // NOTE: Take Bad Stuff should be executed in sequential order, so it is resolved one by one
            foreach (var monster in _monsters)
            {
                await monster.BadStuff(table);
            }

            if (player.IsDead)
            {
                var death = new DeathStep();
                return await death.Resolve(table);
            }

            return table;
        }
    }
}
