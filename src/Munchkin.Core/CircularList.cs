using System.Collections;
using System.Collections.Generic;

namespace Munchkin.Core
{
    public class CircularList<T> : ICollection<T>
    {
        private readonly List<T> _innerList;
        private int _currentHeroIndex;

        public CircularList()
        {
            _innerList = new List<T>();
        }

        public CircularList(IEnumerable<T> list)
        {
            _innerList = new List<T>(list);
        }

        public override string ToString() => $"Count = {_innerList.Count}";

        #region Circular List Implementation

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

        #endregion

        #region IEnumerable<T> Implementation

        public IEnumerator<T> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region ICollection<T> Implementation

        public int Count => _innerList.Count;

        public bool IsReadOnly => false;

        public void Add(T item) => _innerList.Add(item);

        public void Clear() => _innerList.Clear();

        public bool Contains(T item) => _innerList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _innerList.CopyTo(array, arrayIndex);

        public bool Remove(T item) => _innerList.Remove(item);

        #endregion
    }
}
