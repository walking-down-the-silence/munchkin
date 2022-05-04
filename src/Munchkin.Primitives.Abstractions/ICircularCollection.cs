namespace Munchkin.Primitives.Abstractions
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
