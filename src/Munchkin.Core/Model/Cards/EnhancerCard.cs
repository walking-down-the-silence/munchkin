using Munchkin.Core.Model.Properties;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards
{
    public abstract class EnhancerCard : DoorsCard
    {
        protected EnhancerCard(string title, int strengthBonus, int treasureBonus) : base(title)
        {
            AddProperty(new StrengthBonusAttribute(strengthBonus));
            AddProperty(new RewardTreasuresAttribute(treasureBonus));
        }

        public int StrengthBonus => GetProperty<StrengthBonusAttribute>().Bonus;

        public int TreasureBonus => GetProperty<RewardTreasuresAttribute>().Bonus;

        public override Task Play(Table context)
        {
            context.Dungeon.Combat.AddProperty(new MonsterStrengthBonusAttribute(StrengthBonus));
            context.Dungeon.Combat.AddProperty(new RewardTreasuresAttribute(TreasureBonus));
            return Task.CompletedTask;
        }
    }
}