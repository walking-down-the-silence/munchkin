using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Stages
{
    public interface IDecisionSequenceBuilder
    {
        IDecisionTreeContextBuilder Then(IStep<Table> step);
    }
}
