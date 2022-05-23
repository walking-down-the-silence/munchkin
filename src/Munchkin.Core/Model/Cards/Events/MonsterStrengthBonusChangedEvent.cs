using Munchkin.Core.Contracts.Events;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed class MonsterStrengthBonusChangedEvent : EventSupportingAttributes
    {
        public MonsterStrengthBonusChangedEvent(string monsterCardId, int bonus)
        {
            MonsterCardId = monsterCardId;

            AddAttribute(new MonsterStrengthBonusAttribute(bonus));
        }

        public string MonsterCardId { get; }
    }
}
