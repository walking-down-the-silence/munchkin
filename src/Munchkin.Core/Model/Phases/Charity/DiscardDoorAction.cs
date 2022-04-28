using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record DiscardDoorAction(
        Player Player,
        DoorsCard Card) : ICharityAction;
}
