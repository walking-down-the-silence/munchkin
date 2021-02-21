using Munchkin.Core.Model;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Core.Extensions
{
    public static class DungeonExtensions
    {
        public static bool PlayersAreWinningCombat(this Dungeon dungeon)
        {
            int playerStrength = dungeon.CurrentStage.AggregateProperties<PlayerStrengthBonusAttribute>(x => x.Bonus);
            int mosterStrength = dungeon.CurrentStage.AggregateProperties<MonsterStrengthBonusAttribute>(x => x.Bonus);
            return playerStrength > mosterStrength;
        }
    }
}
