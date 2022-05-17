using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Primitives;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Phases
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
    }
}
