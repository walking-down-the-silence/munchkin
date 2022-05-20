using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class MonsterCard : DoorsCard
    {
        protected MonsterCard(string code, string title, int level, int rewardLevels, int rewardTreasures, int runAwayBonus, bool isUndead) : 
            base(code, title)
        {
            AddAttribute(new MonsterStrengthBonusAttribute(level));
            AddAttribute(new RewardLevelsAttribute(rewardLevels));
            AddAttribute(new RewardTreasuresAttribute(rewardTreasures));
            AddAttribute(new RunAwayBonusAttribute(runAwayBonus));

            if (isUndead)
            {
                AddAttribute(new UndeadMonsterAttribute());
            }
        }

        public int Level => GetAttribute<MonsterStrengthBonusAttribute>().Bonus;

        public int RewardLevels => GetAttribute<RewardLevelsAttribute>().Bonus;

        public int RewardTreasures => GetAttribute<RewardTreasuresAttribute>().Bonus;

        public int RunAwayBonus => GetAttribute<RunAwayBonusAttribute>().Bonus;

        public bool IsUndead => GetAttribute<UndeadMonsterAttribute>() != null;

        public override Task Play(Table context)
        {
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return Task.CompletedTask;
        }

        public abstract Task BadStuff(Table context);
    }
}