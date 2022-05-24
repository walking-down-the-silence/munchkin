using Munchkin.Core.Contracts.Events;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Events
{
    public sealed class ClericTurningActionEvent :
        EventSupportingAttributes,
        IClassActionEvent
    {
        public ClericTurningActionEvent(string playerNickname, string discardedCardId)
        {
            PlayerNickname = playerNickname;
            DiscardedCardId = discardedCardId;

            AddAttribute(new PlayerStrengthBonusAttribute(3));
        }

        public string PlayerNickname { get; }

        public string DiscardedCardId { get; }
    }
}