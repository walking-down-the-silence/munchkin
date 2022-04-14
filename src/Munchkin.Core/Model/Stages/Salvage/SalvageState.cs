using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Defines the state when a player avatar has died and the cards needs to be salvaged.
    /// </summary>
    /// <param name="Player">The player whos avatar died.</param>
    /// <param name="Cards">The card collection that needs to be salvaged.</param>
    public record SalvageState(
        Player Player,
        IReadOnlyCollection<Card> Cards);
}
