﻿using Munchkin.Core.Model.Properties;
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
            // TODO: check if current stage is actually a combat
            context.Dungeon.CurrentStage.AddProperty(new MonsterStrengthBonusAttribute(StrengthBonus));
            context.Dungeon.CurrentStage.AddProperty(new RewardTreasuresAttribute(TreasureBonus));
            return Task.CompletedTask;
        }
    }
}