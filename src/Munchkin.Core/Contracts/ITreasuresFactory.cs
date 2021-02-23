using System.Collections.Generic;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Core.Contracts
{
    public interface ITreasuresFactory
    {
        IEnumerable<TreasureCard> GetTreasureCards();
    }
}
