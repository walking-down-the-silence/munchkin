using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record GiveAwayAction(Player Player, TreasureCard Card) :
        ActionBase(TurnActions.Player.GiveAway, "Give The Card", string.Empty),
        ICharityAction;
}
