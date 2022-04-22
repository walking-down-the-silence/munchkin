using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record CombatRoom(
        Table Table,
        Player FightingPlayer,
        Player HelpingPlayer,
        ImmutableList<MonsterCard> Monsters,
        ImmutableList<Card> TemporaryPile
    )
    : StateBase<CombatRoom>(Table, ImmutableList<Attribute>.Empty);

    public static class CombatRoomExtensions
    {
        public static IState From(Table table, Player fightingPlayer, MonsterCard monster)
        {
            return new CombatRoom(
                table,
                fightingPlayer,
                null,
                ImmutableList.Create(monster),
                ImmutableList<Card>.Empty);
        }

        public static IState Curse(this CombatRoom state, CurseCard curse, Player targetPlayer) =>
            CurseExtensions.From(state.Table, targetPlayer, curse, state);

        public static IState AskForHelp(this CombatRoom state) =>
            AskingForHelpExtensions.From(state.Table, state);

        /// <summary>
        /// TODO: invoke card.Play to take effect
        /// </summary>
        /// <param name="state"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static IState Play(this CombatRoom state, Card card) =>
            state with { TemporaryPile = state.TemporaryPile.Add(card) };

        /// <summary>
        /// TODO: run charity for helping player as well
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IState End(this CombatRoom state) =>
            CharityExtensions.From(state.Table, state.FightingPlayer);

        /// <summary>
        /// TODO: introduce the higher-level type for runnniw away for each player from each monster
        /// </summary>
        /// <param name="state"></param>
        /// <param name="monster"></param>
        /// <returns></returns>
        public static IState RunAway(this CombatRoom state, MonsterCard monster) =>
            RunAwayExtensions.From(state.Table, state.FightingPlayer, monster);
    }
}