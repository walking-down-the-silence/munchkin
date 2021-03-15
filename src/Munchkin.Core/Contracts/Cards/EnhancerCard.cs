using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
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
            // TODO: check if current stage is actually a combat
            context.Dungeon.AddProperty(new MonsterStrengthBonusAttribute(StrengthBonus));
            context.Dungeon.AddProperty(new RewardTreasuresAttribute(TreasureBonus));
            return Task.CompletedTask;
        }
    }
}