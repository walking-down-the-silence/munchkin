using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RunAwayStage : State, IStage
    {
        private readonly Player _fightingPlayer;
        private readonly Player _helpingPlayer;
        private readonly IReadOnlyCollection<MonsterCard> _monsters;
        private readonly List<Card> _playedCards;

        public RunAwayStage(Player fightingPlayer, Player helpingPlayer, IReadOnlyCollection<MonsterCard> monsters, List<Card> playedCards)
        {
            _fightingPlayer = fightingPlayer ?? throw new System.ArgumentNullException(nameof(fightingPlayer));
            _helpingPlayer = helpingPlayer ?? throw new System.ArgumentNullException(nameof(helpingPlayer));
            _monsters = monsters ?? throw new System.ArgumentNullException(nameof(monsters));
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<IStage> Resolve(Table table)
        {
            await HandlePlayerDecisionToRunAway(table, _fightingPlayer);
            await HandlePlayerDecisionToRunAway(table, _helpingPlayer);
            return new EmptyStage(_playedCards);
        }

        private async Task HandlePlayerDecisionToRunAway(Table table, Player player)
        {
            var playerRequest = new PlayerRanAwayRequest(player, table);
            var playerResponse = await table.RequestSink.Send(playerRequest);
            var playerAction = await playerResponse.Task;
            bool playerIsRunningAway = playerAction == RanAwayOrContinueActions.RunAway;

            if (playerIsRunningAway)
            {
                var playerRollTheDiceRequest = new RollTheDiceRequest(player);
                var diceRollResponse = await table.RequestSink.Send(playerRollTheDiceRequest);
                var diceRoll = await diceRollResponse.Task;

                if (diceRoll < 5)
                {
                    await TakeBadStuff(table, player);
                }
            }
        }

        private async Task TakeBadStuff(Table table, Player player)
        {
            // TODO: pass a player to take the bad stuff (for a case with helping player)
            var tasks = _monsters.Select(monster => monster.BadStuff(table)).ToArray();
            await Task.WhenAll(tasks);
        }
    }
}
