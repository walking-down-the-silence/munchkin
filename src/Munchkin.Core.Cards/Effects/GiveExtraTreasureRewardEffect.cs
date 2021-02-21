using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Core.Cards.Effects
{
    public class GiveExtraTreasureRewardEffect : IEffect<Table>
    {
        public GiveExtraTreasureRewardEffect(int treasureBonus)
        {
            TreasureBonus = treasureBonus;
        }
        public int TreasureBonus { get; }

        public Table Apply(Table state)
        {
            state.Dungeon.CurrentStage.AddProperty(new RewardTreasuresAttribute(TreasureBonus));
            return state;
        }
    }
}
