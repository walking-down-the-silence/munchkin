using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Expansions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface IExpansionsProvider
    {
        Task<IReadOnlyCollection<ExpansionOption>> GetExpansionOptionsAsync();

        Task<IReadOnlyCollection<IExpansion>> GetExpansionsAsync();
    }
}
