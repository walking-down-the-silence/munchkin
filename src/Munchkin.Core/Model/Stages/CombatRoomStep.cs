using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Actions;
using Munchkin.Core.Model.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CombatRoomStep : HierarchialStep<Table>
    {
        private readonly Player _fightingPlayer;
        private readonly MonsterCard _monsterCard;
        private readonly List<MonsterCard> _monsters;

        public CombatRoomStep(Player fightingPlayer, MonsterCard monsterCard)
        {
            _monsters = new List<MonsterCard> { monsterCard };
            _fightingPlayer = fightingPlayer ?? throw new System.ArgumentNullException(nameof(fightingPlayer));
            _monsterCard = monsterCard ?? throw new System.ArgumentNullException(nameof(monsterCard));

            AskForHelp = new PlayerAskForHelpAction(this);
            OfferHelp = new PlayerOfferHelpAction(this, fightingPlayer);
        }

        #region Actions

        /// <summary>
        /// Gets the dynamic action for asking help in combat.
        /// </summary>
        public IAction<Table> AskForHelp { get; }

        /// <summary>
        /// Gets the dynamic action for offering help in combat.
        /// </summary>
        public IAction<Table> OfferHelp { get; }

        #endregion

        #region Properties

        /// <summary>
        /// A collection of monsters in combat.
        /// </summary>
        public IReadOnlyCollection<MonsterCard> Monsters => _monsters;

        /// <summary>
        /// Gets the player that is currently in combat.
        /// </summary>
        public Player FightingPlayer => _fightingPlayer;

        /// <summary>
        /// Gets the player that agreed to to help in combat.
        /// </summary>
        public Player HelpingPlayer { get; private set; }

        #endregion

        public override async Task<Table> Resolve(Table table)
        {
            // TODO: calculate and set the hero strength and other properties
            table.Dungeon.AddProperty(new PlayerStrengthBonusAttribute(0));
            table.Dungeon.AddProperty(new MonsterStrengthBonusAttribute(_monsterCard.Level));
            table.Dungeon.AddProperty(new RunAwayBonusAttribute(0));
            table.Dungeon.AddProperty(new RewardLevelsAttribute(_monsterCard.RewardLevels));
            table.Dungeon.AddProperty(new RewardTreasuresAttribute(_monsterCard.RewardTreasures));

            // TODO: add "Ask For Help" action to list of available ones

            // NOTE: this stage is blocked until each player agrees to end the combat
            await table.Dungeon.WaitForAllPlayers();

            if (!table.Dungeon.PlayersAreWinningCombat())
            {
                var runAway = new RunAwayStep(FightingPlayer, HelpingPlayer, _monsters);
                return await runAway.Resolve(table);
            }
            else
            {
                // TODO: resolve the good stuff here
            }

            var charity = new CharityStep();
            return await charity.Resolve(table);
        }

        public void HelpPlayerInCombat(Player player)
        {
            HelpingPlayer = player;
        }
    }
}
