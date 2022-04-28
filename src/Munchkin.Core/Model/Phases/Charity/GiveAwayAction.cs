using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record GiveAwayAction(
        Player Player,
        TreasureCard Card) : ICharityAction;
}
