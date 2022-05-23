using Munchkin.Core.Contracts.Events;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed class WarriorBerserkingBonus1Event :
        EventSupportingAttributes,
        IClassActionEvent
    {
        public WarriorBerserkingBonus1Event(string playerNickname, string discardedCardId)
        {
            PlayerNickname = playerNickname;
            DiscardedCardId = discardedCardId;

            AddAttribute(new PlayerStrengthBonusAttribute(1));
        }

        public string PlayerNickname { get; }

        public string DiscardedCardId { get; }
    }
}