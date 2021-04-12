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
                currentStep = _transitionRegister.TransitionFrom(currentStep);
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

        private class TransitionBuilder : ITransitionFromContext
        {
            private readonly TransitionRegister _transitionRegister;

            public TransitionBuilder(TransitionRegister transitionRegister)
            {
                _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
            }

            public ITransitionToContext<TSource> From<TSource>(string stepName)
                where TSource : IStep<Table>
            {
                return new TransitionToBuilder<TSource>(stepName, _transitionRegister);
            }
        }

        private class TransitionToBuilder<TSource> : ITransitionToContext<TSource>
            where TSource : IStep<Table>
        {
            private readonly string _stepName;
            private readonly TransitionRegister _transitionRegister;

            public TransitionToBuilder(string stepName, TransitionRegister transitionRegister)
            {
                _stepName = stepName ?? throw new ArgumentNullException(nameof(stepName));
                _transitionRegister = transitionRegister ?? throw new ArgumentNullException(nameof(transitionRegister));
            }

            public ITransitionToContext<TSource> To<TResult>(
                Func<TSource, TResult> configCreation,
                Func<TSource, bool> configCondition)
                where TResult : IStep<Table>
            {
                var transition = Transition.Create(configCreation, configCondition);
                _transitionRegister.Register<TSource>(_stepName, transition);
                return new TransitionToBuilder<TSource>(_stepName, _transitionRegister);
            }
        }

        private class TransitionRegister
        {
            private readonly Dictionary<string, TransitionHandler> _transitions = new();

            public void Register<TSource>(string fromStepName, Transition transition)
                where TSource : IStep<Table>
            {
                if (transition is null)
                {
                    throw new ArgumentNullException(nameof(transition));
                }

                var handler = _transitions.ContainsKey(fromStepName)
                    ? new TransitionHandler(transition, _transitions[fromStepName])
                    : new TransitionHandler(transition, null);
                _transitions[fromStepName] = handler;
            }

            public IStep<Table> TransitionFrom(IStep<Table> currentStep)
            {
                if (currentStep is null)
                {
                    throw new ArgumentNullException(nameof(currentStep));
                }

                var handler = _transitions.ContainsKey(currentStep.Name)
                    ? _transitions[currentStep.Name]
                    : default;
                return handler?.Handle(currentStep);
            }
        }

        private class TransitionHandler
        {
            private readonly Transition _transition;
            private readonly TransitionHandler _nextHandler;

            public TransitionHandler(Transition current, TransitionHandler next)
            {
                _transition = current ?? throw new ArgumentNullException(nameof(current));
                _nextHandler = next;
            }

            public IStep<Table> Handle(IStep<Table> step) => _transition.CanExecute(step)
                ? _transition.Execute(step)
                : _nextHandler?.Handle(step);
        }
    }
}
