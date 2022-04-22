using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Primitives
{
    public interface IDecisionConditionContext
    {
        IDecisionTreeBuilder Condition(
            Func<Table, Task<bool>> condition,
            Func<IDecisionTreeContext, IDecisionTreeBuilder> branch1,
            Func<IDecisionTreeContext, IDecisionTreeBuilder> branch2);
    }
}
