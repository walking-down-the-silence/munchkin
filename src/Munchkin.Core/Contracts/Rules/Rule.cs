using System;

namespace Munchkin.Core.Contracts.Rules
{
    public static class Rule
    {
        public static IRule<TState> New<TState>(IRule<TState> rule)
        {
            return rule;
        }

        public static IRule<TState> And<TState>(this IRule<TState> left, IRule<TState> right)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            if (right is null) throw new ArgumentNullException(nameof(right));

            return new AndRuleDecorator<TState>(left, right);
        }

        public static IRule<TState> Or<TState>(this IRule<TState> left, IRule<TState> right)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            if (right is null) throw new ArgumentNullException(nameof(right));

            return new OrRuleDecorator<TState>(left, right);
        }
    }
}
