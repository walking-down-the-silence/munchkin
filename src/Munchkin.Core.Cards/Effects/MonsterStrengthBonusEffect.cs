using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Properties;

namespace Munchkin.Core.Cards.Effects
{
    public class MonsterStrengthBonusEffect : IEffect<Table>
    {
        public MonsterStrengthBonusEffect(int bonusStrength)
        {
            BonusStrength = bonusStrength;
        }

        public int BonusStrength { get; }

        public Table Apply(Table state)
        {
            state.Dungeon.Combat.AddProperty(new MonsterStrengthBonusAttribute(BonusStrength));
            return state;
        }
    }
}
