using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class CombatRoomStep : IHierarchialStep<Table>
    {
        private readonly List<MonsterCard> _monsters;
        private readonly MonsterCard _monsterCard;
        private readonly List<Card> _playedCards;

        public CombatRoomStep(MonsterCard monsterCard, List<Card> playedCards)
        {
            _monsters = new List<MonsterCard> { monsterCard };
            _monsterCard = monsterCard ?? throw new System.ArgumentNullException(nameof(monsterCard));
            _playedCards = playedCards ?? throw new System.ArgumentNullException(nameof(playedCards));
        }

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

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<Table> Resolve(Table table)
        {
            // TODO: calculate and set the hero strength and other properties
            table.Dungeon.AddProperty(new PlayerStrengthBonusAttribute(0));
            table.Dungeon.AddProperty(new MonsterStrengthBonusAttribute(_monsterCard.Level));
            table.Dungeon.AddProperty(new RunAwayBonusAttribute(0));
            table.Dungeon.AddProperty(new RewardLevelsAttribute(_monsterCard.RewardLevels));
            table.Dungeon.AddProperty(new RewardTreasuresAttribute(_monsterCard.RewardTreasures));

            // NOTE: this stage is blocked until each player agrees to end the combat
            await table.Dungeon.OngoingCombat();

            if (!table.Dungeon.PlayersAreWinningCombat())
            {
                var runAway = new RunAwayStep(FightingPlayer, HelpingPlayer, _monsters, _playedCards);
                return await  runAway.Resolve(table);
            }
            else
            {
                // TODO: resolve the good stuff here
            }

            var charity = new CharityStep(_playedCards);
            return await charity.Resolve(table);
        }

        public void HelpInCombat(Table table, Player helpingPlayer)
        {
            // TODO: rethink how to initialize actions available to each player
            var playerActions = helpingPlayer.Actions.Select(action => action.Create()).ToList();
            table.Dungeon.SetPlayerActions(helpingPlayer, playerActions);
            HelpingPlayer = helpingPlayer;
        }
    }
}
