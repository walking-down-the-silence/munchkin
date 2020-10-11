using System;

namespace Munchkin.Core
{
    public static class Dice
    {
        private static readonly Random _random = new Random((int)DateTime.Now.Ticks);

        public static int Roll() => _random.Next(6) + 1;
    }
}
