using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Stages.Curse
{
    public record Curse(
        CurseCard Card,
        ImmutableList<Card> PlayedCards)
    {
    }
}
