using System.Collections;
using System.Collections.Generic;

namespace Munchkin.Core
{
    public class CircularList<T> : IEnumerable<T>
    {
        private readonly List<T> _innerList;
        private int _currentHeroIndex;

        public CircularList(IEnumerable<T> list)
        {
            _innerList = new List<T>(list);
        }

        public T Current
        {
            get { return _innerList[_currentHeroIndex]; }
        }

        public T Next()
        {
            _currentHeroIndex = (_currentHeroIndex + 1) % _innerList.Count;
            return _innerList[_currentHeroIndex];
        }

        public T Previous()
        {
            _currentHeroIndex = _currentHeroIndex == 0 ? _innerList.Count - 1 : _currentHeroIndex - 1;
            return _innerList[_currentHeroIndex];
        }

        public T PeekNext()
        {
            int peekNextIndex = (_currentHeroIndex + 1) % _innerList.Count;
            return _innerList[peekNextIndex];
        }

        public T PeekPrevious()
        {
            int peekNextIndex = _currentHeroIndex == 0 ? _innerList.Count - 1 : _currentHeroIndex - 1;
            return _innerList[peekNextIndex];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
