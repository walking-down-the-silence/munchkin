using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Model.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RunAwayStep : StepBase<Table>
    {
        private readonly IReadOnlyCollection<MonsterCard> _monsters;

        public RunAwayStep(
            Player fightingPlayer,
            Player helpingPlayer,
            IReadOnlyCollection<MonsterCard> monsters) : base(StepNames.RunAway)
        {
            FightingPlayer = fightingPlayer ?? throw new System.ArgumentNullException(nameof(fightingPlayer));
            HelpingPlayer = helpingPlayer ?? throw new System.ArgumentNullException(nameof(helpingPlayer));
            _monsters = monsters ?? throw new System.ArgumentNullException(nameof(monsters));
        }
        public Player FightingPlayer { get; }

        public Player HelpingPlayer { get; }

        protected override async Task<Table> OnResolve(Table table)
        {
            table = await HandlePlayerDecisionToRunAway(table, FightingPlayer);
            table = await HandlePlayerDecisionToRunAway(table, HelpingPlayer);
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
