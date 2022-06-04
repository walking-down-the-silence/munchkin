using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed class PlayerStrengthBonusChangedEvent : EventSupportingAttributes
    {
        public PlayerStrengthBonusChangedEvent(string playerNickname, int bonus)
        {
            PlayerNickname = playerNickname;

            AddAttribute(new PlayerStrengthBonusAttribute(bonus));
        }

        public string PlayerNickname { get; }
    }
}
