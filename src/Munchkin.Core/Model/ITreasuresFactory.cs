using System.Collections.Generic;
using Munchkin.Core.Contracts.Cards;

namespace Munchkin.Expansions
{
    public interface ITreasuresFactory
    {
        IEnumerable<TreasureCard> GetTreasureCards();
    }
}
