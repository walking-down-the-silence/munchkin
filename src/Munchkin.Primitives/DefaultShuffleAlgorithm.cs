using Munchkin.Primitives.Abstractions;

namespace Munchkin.Primitives
{
    public class DefaultShuffleAlgorithm<T> : IShuffleAlgorithm<T>
    {
        public void Shuffle(T[] array)
        {
            int count = array.Length;
            var random = new Random((int)DateTime.Now.Ticks);

            while (count > 1)
            {
                count--;
                int randomNumber = random.Next(count + 1);
                (array[count], array[randomNumber]) = (array[randomNumber], array[count]);
            }
        }
    }
}
