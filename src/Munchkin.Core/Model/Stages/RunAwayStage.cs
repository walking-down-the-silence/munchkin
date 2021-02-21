using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RunAwayStage : State, IStage
    {
        private readonly Table _table;
        private readonly Player _fightingPlayer;
        private readonly Player _helpingPlayer;
        private readonly IReadOnlyCollection<MonsterCard> _monsters;

        public RunAwayStage(Table table, Player fightingPlayer, Player helpingPlayer, IReadOnlyCollection<MonsterCard> monsters)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _fightingPlayer = fightingPlayer ?? throw new System.ArgumentNullException(nameof(fightingPlayer));
            _helpingPlayer = helpingPlayer ?? throw new System.ArgumentNullException(nameof(helpingPlayer));
            _monsters = monsters ?? throw new System.ArgumentNullException(nameof(monsters));
        }

        public bool IsTerminal => false;


        public async Task<IStage> Resolve()
        {
            await HandlePlayerDecisionToRunAway(_fightingPlayer);
            await HandlePlayerDecisionToRunAway(_helpingPlayer);
            return new EmptyStage(_table);
        }

        private async Task HandlePlayerDecisionToRunAway(Player player)
        {
            var playerRequest = new RanAwayOrContinueRequest(player, _table);
            var playerResponse = await _table.RequestSink.Send(playerRequest);
            var playerAction = await playerResponse.Task;
            bool playerIsRunningAway = playerAction == RanAwayOrContinueActions.RunAway;

            if (playerIsRunningAway)
            {
                var playerRollTheDiceRequest = new RollTheDiceRequest(player);
                var diceRollResponse = await _table.RequestSink.Send(playerRollTheDiceRequest);
                var diceRoll = await diceRollResponse.Task;

                if (diceRoll < 5)
                {
                    await TakeBadStuff(player);
                }
            }
        }

        private async Task TakeBadStuff(Player player)
        {
            // TODO: pass a player to take the bad stuff (for a case with helping player)
            var tasks = _monsters.Select(monster => monster.BadStuff(_table)).ToArray();
            await Task.WhenAll(tasks);
        }
    }
}
