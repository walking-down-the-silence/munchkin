﻿using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Phases
{
    public class CombatRoomStep : StepBase<Table>
    {
        private readonly Player _fightingPlayer;
        private readonly MonsterCard _monsterCard;
        private readonly List<MonsterCard> _monsters;

        public CombatRoomStep(Player fightingPlayer, MonsterCard monsterCard) : base(StepNames.Combat)
        {
            _monsters = new List<MonsterCard> { monsterCard };
            _fightingPlayer = fightingPlayer ?? throw new System.ArgumentNullException(nameof(fightingPlayer));
            _monsterCard = monsterCard ?? throw new System.ArgumentNullException(nameof(monsterCard));
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

        protected override async Task<Table> OnResolve(Table table)
        {
            // TODO: calculate and set the hero strength and other properties
            //table.Dungeon.AddAtribute(new PlayerStrengthBonusAttribute(0));
            //table.Dungeon.AddAtribute(new MonsterStrengthBonusAttribute(_monsterCard.Level));
            //table.Dungeon.AddAtribute(new RunAwayBonusAttribute(0));
            //table.Dungeon.AddAtribute(new RewardLevelsAttribute(_monsterCard.RewardLevels));
            //table.Dungeon.AddAtribute(new RewardTreasuresAttribute(_monsterCard.RewardTreasures));

            // TODO: add "Ask For Help" action to list of available ones

            // NOTE: this stage is blocked until each player agrees to end the combat

            //if (!table.Dungeon.PlayersAreWinningCombat())
            //{
            //    var runAway = new RunAwayStep(FightingPlayer, HelpingPlayer, _monsters);
            //    return await runAway.Resolve(table);
            //}
            //else
            //{
            //    // TODO: resolve the good stuff here
            //}

            return null;
        }

        public void HelpPlayerInCombat(Player player)
        {
            HelpingPlayer = player;
        }
    }
}