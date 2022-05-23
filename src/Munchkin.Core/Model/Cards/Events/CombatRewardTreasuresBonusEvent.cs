using Munchkin.Core.Contracts.Events;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed class CombatRewardTreasuresBonusEvent : EventSupportingAttributes
    {
        public CombatRewardTreasuresBonusEvent(int bonus)
        {
            AddAttribute(new RewardTreasuresAttribute(bonus));
        }
    }
}
