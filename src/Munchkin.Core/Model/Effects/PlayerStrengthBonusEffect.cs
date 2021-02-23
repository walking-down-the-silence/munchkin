using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Effects
{
    public class PlayerStrengthBonusEffect : IEffect<Table>
    {
        public PlayerStrengthBonusEffect(int bonusStrength)
        {
            BonusStrength = bonusStrength;
        }

        public int BonusStrength { get; }

        public Table Apply(Table state)
        {
            // TODO: check if current stage actually is a combat
            state.Dungeon.CurrentStage.AddProperty(new PlayerStrengthBonusAttribute(BonusStrength));
            return state;
        }
    }
}
