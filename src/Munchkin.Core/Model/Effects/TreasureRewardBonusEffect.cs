using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Effects
{
    public class TreasureRewardBonusEffect : IEffect<Table>
    {
        public TreasureRewardBonusEffect(int treasureBonus)
        {
            TreasureBonus = treasureBonus;
        }
        public int TreasureBonus { get; }

        public Table Apply(Table state)
        {
            //state.Dungeon.AddAtribute(new RewardTreasuresAttribute(TreasureBonus));
            return state;
        }
    }
}
