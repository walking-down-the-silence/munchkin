using System.Collections.Generic;

namespace Munchkin.Core
{
    public interface ICircularCollection<T> : ICollection<T>
    {
        T Current { get; }

        T Next();

        T Previous();

        T PeekNext();

        T PeekPrevious();
    }
}
