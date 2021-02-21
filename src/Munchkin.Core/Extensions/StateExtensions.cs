using Munchkin.Core.Contracts.States;
using System;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class StateExtensions
    {
        public static int AggregateProperties<TAttribute>(this IState state, Func<TAttribute, int> selector)
        {
            return state.Attributes.OfType<TAttribute>().Aggregate(0, (total, next) => total + selector(next));
        }
    }
}
