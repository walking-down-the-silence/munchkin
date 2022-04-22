using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
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
            // TODO: check if current stage actually is a combat
            //context.Dungeon.AddAtribute(new MonsterStrengthBonusAttribute(Level));
            //context.Dungeon.AddAtribute(new RewardLevelsAttribute(RewardLevels));
            //context.Dungeon.AddAtribute(new RewardTreasuresAttribute(RewardTreasures));
            //context.Dungeon.AddAtribute(new RunAwayBonusAttribute(RunAwayBonus));

            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return Task.CompletedTask;
        }

        public abstract Task BadStuff(Table context);
    }
}