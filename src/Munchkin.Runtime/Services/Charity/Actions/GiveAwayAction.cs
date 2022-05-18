using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Runtime.Actions
{
    public record GiveAwayAction(Player Player, TreasureCard Card) :
        ActionBase(TurnActions.Charity.GiveAway, "Give The Card", string.Empty),
        ICharityAction;
}
