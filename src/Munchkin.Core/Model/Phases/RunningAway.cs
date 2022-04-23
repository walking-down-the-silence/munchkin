using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record RunningAway(
        Table Table,
        Player TargetPlayer,
        MonsterCard Monster,
        int DiceRollResult
    ) : StateBase<RunningAway>(Table, TargetPlayer, ImmutableList<Attribute>.Empty)
    {
        public static IState From(Table table, Player targetPlayer, MonsterCard monster)
        {
            return new RunningAway(table, targetPlayer, monster, -1);
        }
    }

    public static class RunningAwayExtensions
    {
        public static IState RollTheDice(this RunningAway state)
        {
            var diceRoll = Dice.Roll();
            return state with { DiceRollResult = diceRoll };
        }

        public static IState ChangeDiceRollWithCard(this RunningAway state, Card card)
        {
            // TODO: think how to actually prompt the user for a desired Dice roll result
            return state with { DiceRollResult = 6 };
        }

        public static IState End(this RunningAway state)
        {
            return Charity.From(state.Table, state.TargetPlayer);
        }

        public static IState TakeBadStuff(this RunningAway state)
        {
            state.Monster.BadStuff(state.Table);
            return state.TargetPlayer.IsDead
                ? LootingTheBody.From(state.Table)
                : Charity.From(state.Table, state.TargetPlayer);
        }
    }
}