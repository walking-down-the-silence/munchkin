using System;
using System.Collections.Generic;

namespace Munchkin.Core.Extensions
{
    public static class CommonExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action.Invoke(item);
            }
        }
    }
}
