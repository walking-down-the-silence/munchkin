using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Model.Phases
{
    public record PlayCardAction(
        Player Player,
        Card Card) : IDungeonAction;
}