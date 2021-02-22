using Munchkin.Core.Model.Cards;
using System.Collections.Generic;

namespace Munchkin.Expansions
{
    public interface IDoorsFactory
    {
        IEnumerable<DoorsCard> GetDoorsCards();
    }
}