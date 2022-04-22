using Munchkin.Core.Contracts.Actions;

namespace Munchkin.Core.Contracts.Rules
{
    public class EffectUnderCondition<TState> : IConditionalEffect<TState>
    {
        private readonly IEffect<TState> _effect;
        private readonly IRule<TState> _rule;

        public EffectUnderCondition(IEffect<TState> effect, IRule<TState> rule)
        {
            _effect = effect ?? throw new System.ArgumentNullException(nameof(effect));
            _rule = rule ?? throw new System.ArgumentNullException(nameof(rule));
        }

        public TState Apply(TState state) => _effect.Apply(state);

        public bool Satisfies(TState state) => _rule.Satisfies(state);
    }
}
