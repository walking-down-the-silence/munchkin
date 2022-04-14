using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Collections.Generic;

namespace Munchkin.Core.Stages
{
    /// <summary>
    /// Defines the state on a trading party.
    /// </summary>
    /// <param name="Player">The player that is trading.</param>
    /// <param name="Cards">The cards collection the player is willing to trade.</param>
    public record TradingSide(
        Player Player,
        IReadOnlyCollection<Card> Cards);
}
