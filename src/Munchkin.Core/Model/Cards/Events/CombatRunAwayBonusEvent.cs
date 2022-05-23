using Munchkin.Core.Contracts.Events;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed class CombatRunAwayBonusEvent : EventSupportingAttributes
    {
        public CombatRunAwayBonusEvent(string playerNickname, int bonus)
        {
            PlayerNickname = playerNickname;

            AddAttribute(new RunAwayBonusAttribute(bonus));
        }

        public string PlayerNickname { get; }
    }
}
