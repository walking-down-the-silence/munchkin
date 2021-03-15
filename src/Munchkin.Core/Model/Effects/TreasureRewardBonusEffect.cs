using Munchkin.Core.Contracts;
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
            state.Dungeon.AddProperty(new RewardTreasuresAttribute(TreasureBonus));
            return state;
        }
    }
}
