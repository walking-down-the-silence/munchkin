using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CombatStage : State, IStage
    {
        private readonly Table _table;
        private readonly List<MonsterCard> _monsters;
        private readonly List<Card> _playedCards;

        public CombatStage(Table table, MonsterCard monsterCard, List<Card> playedCards)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _monsters = new List<MonsterCard> { monsterCard };
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));

            // TODO: calculate and set the hero strength and other properties
            AddProperty(new PlayerStrengthBonusAttribute(0));
            AddProperty(new MonsterStrengthBonusAttribute(monsterCard.Level));
            AddProperty(new RunAwayBonusAttribute(0));
            AddProperty(new RewardLevelsAttribute(monsterCard.RewardLevels));
            AddProperty(new RewardTreasuresAttribute(monsterCard.RewardTreasures));
        }

        #region Attribute Bonuses

        /// <summary>
        /// Monster strength if any present.
        /// </summary>
        public int MonsterStrength => this.AggregateProperties<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Heroes strength that entered the dungeon.
        /// </summary>
        public int PlayersStrength => this.AggregateProperties<PlayerStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Bonus to run away from dungeon.
        /// </summary>
        public int RunAwayBonus => this.AggregateProperties<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public int RewardLevels => this.AggregateProperties<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public int RewardTreasures => this.AggregateProperties<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion

        /// <summary>
        /// A collection of monsters in combat.
        /// </summary>
        public IReadOnlyCollection<MonsterCard> Monsters => _monsters;

        /// <summary>
        /// Gets the player that is currently in combat.
        /// </summary>
        public Player FightingPlayer { get; private set; }

        /// <summary>
        /// Gets the player that agreed to to help in combat.
        /// </summary>
        public Player HelpingPlayer { get; private set; }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<IStage> Resolve()
        {
            // TODO: immplement the loop of an actual combat actions
            if (!_table.Dungeon.PlayersAreWinningCombat())
            {
                return new RunAwayStage(_table, FightingPlayer, HelpingPlayer, _monsters, _playedCards);
            }

            return new EndStage(_table, _playedCards);
        }

        public void HelpInCombat(Player helpingPlayer)
        {
            // TODO: rethink how to initialize actions available to each player
            var playerActions = helpingPlayer.Actions.Select(action => action.Create()).ToList();
            _table.Dungeon.SetPlayerActions(helpingPlayer, playerActions);
            HelpingPlayer = helpingPlayer;
        }
    }
}
