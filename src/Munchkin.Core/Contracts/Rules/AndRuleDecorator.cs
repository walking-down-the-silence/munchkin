﻿namespace Munchkin.Core.Contracts.Rules
{
    public class AndRuleDecorator<TState> : IRule<TState>
    {
        private readonly IRule<TState> _leftRule;
        private readonly IRule<TState> _rightRule;

        public AndRuleDecorator(IRule<TState> leftRule, IRule<TState> rightRule)
        {
            _leftRule = leftRule ?? throw new System.ArgumentNullException(nameof(leftRule));
            _rightRule = rightRule ?? throw new System.ArgumentNullException(nameof(rightRule));
        }

        public bool Satisfies(TState state)
        {
            return _leftRule.Satisfies(state) && _rightRule.Satisfies(state);
        }
    }
}
