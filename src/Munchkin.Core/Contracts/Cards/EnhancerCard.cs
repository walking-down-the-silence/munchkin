using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class EnhancerCard : DoorsCard
    {
        protected EnhancerCard(string code, string title, int strengthBonus, int treasureBonus) : 
            base(code, title)
        {
            AddAttribute(new StrengthBonusAttribute(strengthBonus));
            AddAttribute(new RewardTreasuresAttribute(treasureBonus));
        }

        public int StrengthBonus => GetAttribute<StrengthBonusAttribute>().Bonus;

        public int TreasureBonus => GetAttribute<RewardTreasuresAttribute>().Bonus;

        public override Task Play(Table context)
        {
            // TODO: check if current stage is actually a combat
            //context.Dungeon.AddAtribute(new MonsterStrengthBonusAttribute(StrengthBonus));
            //context.Dungeon.AddAtribute(new RewardTreasuresAttribute(TreasureBonus));
            return Task.CompletedTask;
        }
    }
}