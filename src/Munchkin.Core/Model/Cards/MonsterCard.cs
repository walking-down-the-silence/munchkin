using Munchkin.Core.Model.Properties;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards
{
    public abstract class MonsterCard : DoorsCard
    {
        protected MonsterCard(string title, int level, int rewardLevels, int rewardTreasures, int runAwayBonus, bool isUndead) : base(title)
        {
            AddProperty(new MonsterStrengthBonusAttribute(level));
            AddProperty(new RewardLevelsAttribute(rewardLevels));
            AddProperty(new RewardTreasuresAttribute(rewardTreasures));
            AddProperty(new RunAwayBonusAttribute(runAwayBonus));

            if (isUndead)
            {
                AddProperty(new UndeadMonsterAttribute());
            }
        }

        public int Level => GetProperty<MonsterStrengthBonusAttribute>().Bonus;

        public int RewardLevels => GetProperty<RewardLevelsAttribute>().Bonus;

        public int RewardTreasures => GetProperty<RewardTreasuresAttribute>().Bonus;

        public int RunAwayBonus => GetProperty<RunAwayBonusAttribute>().Bonus;

        public bool IsUndead => GetProperty<UndeadMonsterAttribute>() != null;

        public override Task Play(Table context)
        {
            context.Dungeon.Combat.AddProperty(new MonsterStrengthBonusAttribute(Level));
            context.Dungeon.Combat.AddProperty(new RewardLevelsAttribute(RewardLevels));
            context.Dungeon.Combat.AddProperty(new RewardTreasuresAttribute(RewardTreasures));
            context.Dungeon.Combat.AddProperty(new RunAwayBonusAttribute(RunAwayBonus));
            return Task.CompletedTask;
        }

        public abstract Task BadStuff(Table context);
    }
}