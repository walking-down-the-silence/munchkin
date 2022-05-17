namespace Munchkin.Primitives.Abstractions
{
    public interface IShuffleAlgorithm<in T>
    {
        void Shuffle(T[] array);
    }
}
