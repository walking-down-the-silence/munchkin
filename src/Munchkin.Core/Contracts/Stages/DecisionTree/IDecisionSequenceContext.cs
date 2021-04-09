using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Stages
{
    public interface IDecisionSequenceContext
    {
        IDecisionTreeContext Then(IStep<Table> step);
    }
}
