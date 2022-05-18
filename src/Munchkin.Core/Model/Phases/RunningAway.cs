using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Phases.Events;

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

            table.ActionLog.Add(diceRollEvent);

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
}