using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Runtime.Actions;
using Munchkin.Runtime.Services;

namespace Munchkin.Core.Model.Phases
{
    public record DiscardTreasureAction(Player Player, TreasureCard Card) :
        ActionBase(TurnActions.Player.DiscardCard, "Discard The Treasure Card", string.Empty),
        ICharityAction;
}
