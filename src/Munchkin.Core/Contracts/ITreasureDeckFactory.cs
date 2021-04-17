using System.Collections.Generic;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Contracts
{
    public interface ITreasureDeckFactory
    {
        IReadOnlyCollection<TreasureCard> GetTreasureCards();
    }
}
