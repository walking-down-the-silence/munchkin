using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record LootTheBody(
        Player Player,
        Card Card) : IDeathAction;
}
