using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record Combat(
        Table Table,
        Player FightingPlayer,
        Player HelpingPlayer,
        AskingForHelp AskingForHelp,
        ImmutableArray<RunningAway> RunningAways,
        ImmutableArray<Card> TemporaryPile)
    {
        public static Combat From(Table table, Player currentPlayer)
        {
            var askingForHelp = AskingForHelp.From(table);
            return new Combat(
                table,
                currentPlayer,
                null,
                askingForHelp,
                ImmutableArray<RunningAway>.Empty,
                ImmutableArray<Card>.Empty);
        }

        public static Combat Reduce(Combat state, ICombatAction action)
        {
            return action switch
            {
                RunAwayAction _             => RunAway(state),
                RewardAction _              => Reward(state),
                AskForHelpAction askForHelp => AskForHelp(state, askForHelp.AskedPlayer),
                AcceptHelpAction acceptHelp => AcceptHelpRequest(state, acceptHelp.AskedPlayer),
                RejectHelpAction _          => RejectHelpRequesst(state),
                _                           => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        #region Calculated Properties

        public IEnumerable<MonsterCard> Monsters => TemporaryPile.OfType<MonsterCard>();

        public IEnumerable<Card> MonsterEnhancers => TemporaryPile
            .Where(x => x.HasAttribute<StrengthBonusAttribute>() || x.HasAttribute<MonsterStrengthBonusAttribute>())
            .Where(x => x.BoundTo is MonsterCard);

        public IEnumerable<Card> PlayerEnhancers => TemporaryPile
            .Where(x => x.HasAttribute<StrengthBonusAttribute>())
            .Where(x => x.BoundTo is null);

        public int MonstersStrength()
        {
            var enhancers = MonsterEnhancers.Aggregate(0, (total, card) =>
            {
                var strength = card.AggregateAttributes<StrengthBonusAttribute>(x => x.Bonus);
                return total + strength;
            });
            var levels = Monsters.Aggregate(0, (totalStrength, monster) => totalStrength + monster.Level);
            return levels + enhancers;
        }

        public int PlayersStrength()
        {
            var enhancers = PlayerEnhancers.Aggregate(0, (total, card) =>
            {
                var strength = card.AggregateAttributes<StrengthBonusAttribute>(x => x.Bonus);
                return total + strength;
            });
            return FightingPlayer.Level + HelpingPlayer.Level + enhancers;
        }

        #endregion

        #region Combat Actions

        /// <summary>
        /// TODO: run charity for helping player as well
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static Combat Reward(Combat state)
        {
            // TODO: collect all the treasures, levels and other stuff after a combat
            // TODO: do the same for helping player on agreed treasures
            var rewardTreasures = state.Monsters.Aggregate(0, (total, monster) => total + monster.RewardTreasures);
            var rewardLevels = state.Monsters.Aggregate(0, (total, monster) => total + monster.RewardLevels);

            state.FightingPlayer.LevelUp(rewardLevels);
            state.HelpingPlayer?.LevelUp(rewardLevels);

            // TODO: think of a way to distribute treasures based on help agreement
            state.Table.TreasureCardDeck.Take(rewardTreasures)
                .ForEach(card => state.FightingPlayer.TakeInHand(card));

            //var nextState = Charity.From(turn.Table, turn.CurrentPlayer);
            var availableActions = ImmutableList.CreateRange(new[]
            {
                TurnActions.Player.DiscardDoor,
                TurnActions.Player.DiscardTreasure,
                TurnActions.Player.GiveAway
            });
            //return ActionResult.Create<Combat>(null, availableActions);
            return state;
        }

        /// <summary>
        /// TODO: introduce the higher-level type for runnniw away for each player from each monster
        /// </summary>
        /// <param name="state"></param>
        /// <param name="monster"></param>
        /// <returns></returns>
        public static Combat RunAway(Combat state)
        {
            // TODO: should be called for all monsters per each player (fighting and helping)
            var nextState = ImmutableArray.CreateRange(state.Monsters.SelectMany(monster => new[]
            {
                RunningAway.From(state.Table, state.FightingPlayer, monster, -1),
                RunningAway.From(state.Table, state.HelpingPlayer, monster, -1)
            }));
            var availableActions = ImmutableList.CreateRange(new[]
            {
                TurnActions.Dungeon.PlayCard,
                TurnActions.Combat.RollTheDice,
                TurnActions.Combat.TakeBadStuffFromMonster
            });

            //return ActionResult.Create(state, availableActions);
            return state;
        }

        #endregion

        #region Asking For Help

        /// <summary>
        /// Sends a request for rhelp to a selected player.
        /// TODO: send a request for help to the target player
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetPlayer"></param>
        /// <returns></returns>
        public static Combat AskForHelp(Combat state, Player targetPlayer)
        {
            //return ActionResult.Create(state, null, null);
            return state with
            {
                HelpingPlayer = null,
                AskingForHelp = state.AskingForHelp.WithAskedPlayer(targetPlayer)
            };
        }

        /// <summary>
        /// Accepts request for help.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="askedPlayer"></param>
        /// <returns></returns>
        public static Combat AcceptHelpRequest(Combat state, Player askedPlayer)
        {
            return state with
            {
                HelpingPlayer = askedPlayer,
                AskingForHelp = state.AskingForHelp.WithoutAskedPlayer()
            };
        }

        /// <summary>
        /// Rejects rerquest for help.
        /// TODO: remove player that rejected from the list of players to ask
        /// </summary>
        /// <param name="state"></param>
        /// <param name="askedPlayer"></param>
        /// <returns></returns>s
        public static Combat RejectHelpRequesst(Combat state)
        {
            return state with
            {
                HelpingPlayer = null,
                AskingForHelp = state.AskingForHelp.WithoutAskedPlayer()
            };
        }

        #endregion
    }
}
