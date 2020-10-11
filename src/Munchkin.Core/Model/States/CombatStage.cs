using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Properties;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Model
{
    public class CombatStage : State
    {
        private readonly List<MonsterCard> _monsters;

        private CombatStage(Dungeon dungeon, MonsterCard monster)
        {
            Dungeon = dungeon ?? throw new System.ArgumentNullException(nameof(dungeon));
            LastCardPlayed = monster ?? throw new System.ArgumentNullException(nameof(monster));
            _monsters = new List<MonsterCard> { monster };

            // TODO: calculate and set the hero strength and other properties
            AddProperty(new PlayerStrengthBonusAttribute(0));
            AddProperty(new MonsterStrengthBonusAttribute(monster.Level));
            AddProperty(new RunAwayBonusAttribute(0));
            AddProperty(new RewardLevelsAttribute(monster.RewardLevels));
            AddProperty(new RewardTreasuresAttribute(monster.RewardTreasures));
        }

        public static CombatStage EnterCombat(Dungeon dungeon, MonsterCard monster)
        {
            return new CombatStage(dungeon, monster);
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
        /// The dungeon in hich current combat takes place.
        /// </summary>
        public Dungeon Dungeon { get; }

        /// <summary>
        /// A collection of monsters in combat.
        /// </summary>
        public IReadOnlyCollection<MonsterCard> Monsters => _monsters;

        /// <summary>
        /// Player that agreed to enter the dungeon to help
        /// </summary>
        public Player HelpingPlayer { get; private set; }

        public Card LastCardPlayed { get; private set; }

        public void HelpPlayer(Player helpingPlayer)
        {
            var playerActions = helpingPlayer.Actions.Select(action => action.Create()).ToList();
            Dungeon.SetPlayerActions(helpingPlayer, playerActions);
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
