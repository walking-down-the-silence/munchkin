using Munchkin.Core.Contracts.Rules;
using System;

namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Extensions for fluent configuration of the conditional effects.
    /// </summary>
    public static class Effect
    {
        /// <summary>
        /// A wrapper method for checking against null values and chaining othe methods in a fluent fashion.
        /// </summary>
        public static IEffect<TState> New<TState>(IEffect<TState> effect)
        {
            if (effect is null) throw new ArgumentNullException(nameof(effect));

            return effect;
        }

        /// <summary>
        /// Wraps the original effect into a conditional effect with rules to check.
        /// </summary>
        public static IConditionalEffect<TState> With<TState>(this IEffect<TState> effect, Func<IRule<TState>> configure)
        {
            if (effect is null) throw new ArgumentNullException(nameof(effect));

            var rule = configure.Invoke();
            return new EffectUnderCondition<TState>(effect, rule);
        }
    }
}
