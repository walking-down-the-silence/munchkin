using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Extensions.Threading
{
    public static class TaskExtensions
    {
        public static Task<TSource> Unit<TSource>(this TSource source)
        {
            return Task.FromResult(source);
        }

        public static async Task<TTarget> Select<TSource, TTarget>(
            this Task<TSource> source,
            Func<TSource, TTarget> selector)
        {
            var result = await source;
            var transformed = selector(result);
            return transformed;
        }

        public static async Task<TTarget> SelectMany<TSource, TTarget>(
            this Task<TSource> source,
            Func<TSource, Task<TTarget>> selector)
        {
            var result = await source;
            var transformed = await selector(result);
            return transformed;
        }

        public static async Task<TTarget> Aggregate<TSource, TTarget>(
            this IEnumerable<TSource> source,
            TTarget seed,
            Func<TTarget, TSource, Task<TTarget>> aggregator)
        {
            foreach (var item in source)
            {
                seed = await aggregator(seed, item);
            }

            return seed;
        }
    }
}
