using System;
using System.Threading;

namespace Munchkin.Core
{
    public static class Dice
    {
        private static int _sides = 6;
        private static readonly Random _random = new((int)DateTime.Now.Ticks);

        public static void Reset(int sides) => Interlocked.Exchange(ref _sides, sides);

        public static int Roll() => _random.Next(_sides) + 1;
    }
}
