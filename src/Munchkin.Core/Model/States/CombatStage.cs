using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Properties;
using Munchkin.Core.Model.States;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class CombatStage : State, IStage
    {
        private readonly Table _table;
        private readonly MonsterCard _monsterCard;
        private readonly List<MonsterCard> _monsters;

        public CombatStage(Table table, MonsterCard monsterCard)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            _monsters = new List<MonsterCard> { monsterCard };
            _monsterCard = monsterCard ?? throw new System.ArgumentNullException(nameof(monsterCard));

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
        public int MonsterStrength => AggregateProperties<MonsterStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Heroes strength that entered the dungeon.
        /// </summary>
        public int PlayersStrength => AggregateProperties<PlayerStrengthBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Bonus to run away from dungeon.
        /// </summary>
        public int RunAwayBonus => AggregateProperties<RunAwayBonusAttribute>(x => x.Bonus);

        /// <summary>
        /// Levels gained if dungeon is cleared.
        /// </summary>
        public int RewardLevels => AggregateProperties<RewardLevelsAttribute>(x => x.Bonus);

        /// <summary>
        /// Treasures gained if dungeon is cleared.
        /// </summary>
        public int RewardTreasures => AggregateProperties<RewardTreasuresAttribute>(x => x.Bonus);

        #endregion

        /// <summary>
        /// A collection of monsters in combat.
        /// </summary>
        public IReadOnlyCollection<MonsterCard> Monsters => _monsters;

        /// <summary>
        /// Player that agreed to enter the dungeon to help
        /// </summary>
        public Player HelpingPlayer { get; private set; }

        public bool IsTerminal => false;

        public async Task<IStage> Resolve()
        {
            // TODO: prompt the player if they want to run away or take bad stuff
            bool isRunningAway = false;

            if (isRunningAway)
            {
                return new RunAwayStage(_table);
            }

            // TODO: take bad stuff
            return new EndStage(_table);
        }

        public void HelpPlayer(Player helpingPlayer)
        {
            var playerActions = helpingPlayer.Actions.Select(action => action.Create()).ToList();
            _table.Dungeon.SetPlayerActions(helpingPlayer, playerActions);
            HelpingPlayer = helpingPlayer;
        }

        public bool AnyCardsPlayed()
        {
            System.Console.WriteLine(nameof(AnyCardsPlayed));
            return true;
        }

        public bool PlayersAreWinningCombat()
        {
            System.Console.WriteLine(nameof(PlayersAreWinningCombat));
            bool winning = true;
            return winning;
        }

        public CombatStage TakeGoodStuff()
        {
            System.Console.WriteLine(nameof(TakeGoodStuff));
            return this;
        }

        public CombatStage PromptUserToPlayCards()
        {
            System.Console.WriteLine(nameof(PromptUserToPlayCards));
            return this;
        }

        public CombatStage Recalculate()
        {
            System.Console.WriteLine(nameof(Recalculate));
            return this;
        }
    }
}
