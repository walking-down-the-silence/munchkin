using Munchkin.Core.Model;
using System;

namespace Munchkin.Core.Contracts.Stages
{
    public class Transition
    {
        private readonly string _sourceStepName;
        private readonly string _targetStepName;
        private readonly Func<IStep<Table>, IStep<Table>> _creator;
        private readonly Func<IStep<Table>, bool> _condition;

        private Transition(
            string sourceStepName,
            string targetStepName,
            Func<IStep<Table>, IStep<Table>> creator,
            Func<IStep<Table>, bool> condition)
        {
            if (string.IsNullOrEmpty(sourceStepName))
            {
                throw new ArgumentException($"'{nameof(sourceStepName)}' cannot be null or empty.", nameof(sourceStepName));
            }

            if (string.IsNullOrEmpty(targetStepName))
            {
                throw new ArgumentException($"'{nameof(targetStepName)}' cannot be null or empty.", nameof(targetStepName));
            }

            _sourceStepName = sourceStepName;
            _targetStepName = targetStepName;
            _creator = creator ?? throw new ArgumentNullException(nameof(creator));
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public static Transition Create<TSource, TTarget>(
            Func<TSource, TTarget> configCreation,
            Func<TSource, bool> configCondition)
            where TSource : IStep<Table>
            where TTarget : IStep<Table>
        {
            if (configCreation is null)
            {
                throw new ArgumentNullException(nameof(configCreation));
            }

            if (configCondition is null)
            {
                throw new ArgumentNullException(nameof(configCondition));
            }

            Func<IStep<Table>, IStep<Table>> creator = (s) => configCreation.Invoke((TSource)s);
            Func<IStep<Table>, bool> condition = (s) => configCondition.Invoke((TSource)s);
            return new Transition(nameof(TSource), nameof(TTarget), creator, condition);
        }

        public bool CanExecute(IStep<Table> currentStep)
        {
            if (currentStep is null)
            {
                throw new ArgumentNullException(nameof(currentStep));
            }

            return _condition.Invoke(currentStep);
        }

        public IStep<Table> Execute(IStep<Table> currentStep)
        {
            if (currentStep is null)
            {
                throw new ArgumentNullException(nameof(currentStep));
            }

            var result = _condition.Invoke(currentStep)
                ? _creator.Invoke(currentStep)
                : default;

            return result;
        }
    }
}
