using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;

namespace Munchkin.Core.Contracts
{
    public interface IDoorDeckFactory
    {
        IReadOnlyCollection<DoorsCard> GetDoorsCards();
    }
}