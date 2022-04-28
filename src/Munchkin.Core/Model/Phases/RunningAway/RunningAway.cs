using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record RunningAway(
        Table Table,
        Player Player,
        MonsterCard Monster,
        int DiceRollResult,
        ImmutableArray<Card> TemporaryPile)
    {
        public static RunningAway From(Table table, Player targetPlayer, MonsterCard monster, int diceRoll)
        {
            return new RunningAway(
                table,
                targetPlayer,
                monster,
                diceRoll,
                ImmutableArray<Card>.Empty);
        }

        public static RunningAway Reduce(RunningAway state, IRunningAwayAction action)
        {
            return action switch
            {
                RollTheDiceAction rollTheDice               => RollTheDice(state, rollTheDice.Player, state.Monster),
                TakeBadStuffFromMonsterAction takeBadStuff  => TakeBadStuff(state, takeBadStuff.Player, state.Monster),
                _                                           => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        public static RunningAway RollTheDice(RunningAway state, Player player, MonsterCard monster)
        {
            state = state with { DiceRollResult = Dice.Roll() };

            var availableActions = ImmutableList.CreateRange(new[]
            {
                TurnActions.Player.DiscardDoor,
                TurnActions.Player.DiscardTreasure,
                TurnActions.Player.GiveAway
            });
            //return ActionResult.Create<RunningAway>(null, availableActions);
            return state;
        }

        public static RunningAway TakeBadStuff(RunningAway state, Player player, MonsterCard monster)
        {
            // TODO: pass the player implicitly
            if (!player.IsDead)
            {
                monster.BadStuff(state.Table);
            }

            var availableActions = ImmutableList.CreateRange(player.IsDead
                ? TurnActions.Death.All
                : TurnActions.Player.All);
            //return ActionResult.Create<RunningAway>(null, availableActions);
            return state;
        }
    }
}