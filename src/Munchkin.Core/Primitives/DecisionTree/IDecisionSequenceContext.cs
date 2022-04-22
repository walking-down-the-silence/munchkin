using Munchkin.Core.Model;
using Munchkin.Core.Primitives;

namespace Munchkin.Core.Primitives
{
    public interface IDecisionSequenceContext
    {
        IDecisionTreeContext Then(IStep<Table> step);
    }
}
