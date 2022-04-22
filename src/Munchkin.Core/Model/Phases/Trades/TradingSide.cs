using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases.Trades
{
    /// <summary>
    /// Defines the state on a trading party.
    /// </summary>
    /// <param name="Player">The player that is trading.</param>
    /// <param name="OfferedCards">The cards collection the player is willing to trade.</param>
    public record TradingSide(
        Player Player,
        ImmutableList<Card> OfferedCards);
}
