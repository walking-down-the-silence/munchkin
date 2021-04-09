using Munchkin.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public class DecisionGraph
    {
        private readonly TransitionRegister _transitionRegister;

        private DecisionGraph(TransitionRegister transitionRegister)
        {
            _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
        }

        public static ITransitionGraphContext Empty()
        {
            return new DecisionGraphBuilder();
        }

        public async Task<Table> Resolve(Table table, IStep<Table> step)
        {
            var currentStep = step;

            while (currentStep != null)
            {
                table = await currentStep.Resolve(table);
                var transition = _transitionRegister.GetTransition(currentStep.GetType());
                currentStep = transition.CanExecute(currentStep)
                    ? transition.Execute(currentStep)
                    : default;
            }

            return table;
        }

        private class DecisionGraphBuilder : ITransitionGraphContext
        {
            private readonly TransitionRegister _transitionRegister;

            public DecisionGraphBuilder()
            {
                _transitionRegister = new TransitionRegister();
            }

            public DecisionGraphBuilder(TransitionRegister transitionRegister)
            {
                _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
            }

            public DecisionGraph Build() => new(_transitionRegister);

            public ITransitionGraphContext Transition(Action<ITransitionFromContext> transiftionConfig)
            {
                if (transiftionConfig is null)
                {
                    throw new ArgumentNullException(nameof(transiftionConfig));
                }

                var transitionBuilder = new TransitionBuilder(_transitionRegister);
                transiftionConfig.Invoke(transitionBuilder);
                return new DecisionGraphBuilder(_transitionRegister);
            }
        }

        private class TransitionRegister
        {
            private readonly Dictionary<Type, Transition> _transitions = new();

            public void Register<TSource>(Transition transition)
                where TSource : IStep<Table>
            {
                if (transition is null)
                {
                    throw new ArgumentNullException(nameof(transition));
                }

                _transitions[typeof(TSource)] = transition;
            }

            public Transition GetTransition(Type fromStepType)
            {
                if (fromStepType is null)
                {
                    throw new ArgumentNullException(nameof(fromStepType));
                }

                return _transitions.ContainsKey(fromStepType)
                    ? _transitions[fromStepType]
                    : default;
            }
        }

        private class TransitionBuilder : ITransitionFromContext
        {
            private readonly TransitionRegister _transitionRegister;

            public TransitionBuilder(TransitionRegister transitionRegister)
            {
                _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
            }

            public ITransitionToContext<TSource> From<TSource>()
                where TSource : IStep<Table>
            {
                return new TransitionToBuilder<TSource>(_transitionRegister);
            }
        }

        private class TransitionToBuilder<TSource> : ITransitionToContext<TSource>
            where TSource : IStep<Table>
        {
            private readonly TransitionRegister _transitionRegister;

            public TransitionToBuilder(TransitionRegister transitionRegister)
            {
                _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
            }

            public ITransitionToContext<TSource> To<TResult>(Func<TSource, TResult> configCreation, Func<TSource, bool> configCondition)
                where TResult : IStep<Table>
            {
                var transition = Transition.Create(configCreation, configCondition);
                _transitionRegister.Register<TSource>(transition);
                return new TransitionToBuilder<TSource>(_transitionRegister);
            }
        }
    }
}
