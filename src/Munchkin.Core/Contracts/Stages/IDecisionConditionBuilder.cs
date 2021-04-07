using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public interface IDecisionConditionBuilder
    {
        IDecisionTreeBuilder Condition(
            Func<Table, Task<bool>> condition,
            Func<IDecisionTreeContextBuilder, IDecisionTreeBuilder> branch1,
            Func<IDecisionTreeContextBuilder, IDecisionTreeBuilder> branch2);
    }
}
