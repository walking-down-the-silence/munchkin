using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;

namespace Munchkin.Core.Contracts
{
    public interface IDoorsFactory
    {
        IEnumerable<DoorsCard> GetDoorsCards();
    }
}