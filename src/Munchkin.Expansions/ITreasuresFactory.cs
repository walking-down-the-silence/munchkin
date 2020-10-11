using System.Collections.Generic;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Expansions
{
    public interface ITreasuresFactory
    {
        IEnumerable<TreasureCard> GetTreasureCards();
    }
}
