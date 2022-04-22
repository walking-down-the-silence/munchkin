using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record RunAway(
        Table Table,
        Player TargetPlayer,
        MonsterCard Monster,
        int DiceRollResult
    )
    : StateBase<RunAway>(Table, ImmutableList<Attribute>.Empty);

    public static class RunAwayExtensions
    {
        public static IState From(Table table, Player targetPlayer, MonsterCard monster)
        {
            return new RunAway(table, targetPlayer, monster, -1);
        }

        public static IState RollTheDice(this RunAway state)
        {
            var diceRoll = Dice.Roll();
            return state with { DiceRollResult = diceRoll };
        }

        public static IState ChangeDiceRollWithCard(this RunAway state)
        {
            // TODO: think how to actually prompt the user for a desired Dice roll result
            return state with { DiceRollResult = 6 };
        }
    }
}