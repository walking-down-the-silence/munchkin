﻿using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Effects
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
            // TODO: check if current stage actually is a combat
            //state.Dungeon.AddAtribute(new MonsterStrengthBonusAttribute(BonusStrength));
            return state;
        }
    }
}
