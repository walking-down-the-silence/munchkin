using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;

namespace Munchkin.Core.Model.Phases
{
    public static class RunningAway
    {
        public static Table RollTheDice(Table table, MonsterCard monster, Player player)
        {
            // TODO: get real monster card id
            var diceRollEvent = new RunningAwayFromMonsterDiceRollEvent(
                player.Nickname,
                monster.GetHashCode().ToString(),
                Dice.Roll());

            table.ActionLog.Push(diceRollEvent);

            return table;
        }

        public static Table TakeBadStuff(Table table, MonsterCard monster, Player taker)
        {
            if (!taker.IsDead())
            {
                monster.BadStuff(table);
            }

            return table;
        }
    }

    public record RunningAwayFromMonsterDiceRollEvent(
        string PlayerNickname,
        string MonsterCardId,
        int DiceRollResult);
}