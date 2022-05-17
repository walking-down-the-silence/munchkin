using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Expansions;
using Munchkin.Runtime.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Repositories
{
    public class ExpansionsProvider : IExpansionsProvider
    {
        private readonly IServiceProvider _expansionProvider;

        public ExpansionsProvider(
            IServiceProvider expansionProvider)
        {
            _expansionProvider = expansionProvider ?? throw new ArgumentNullException(nameof(expansionProvider));
        }

        public Task<IReadOnlyCollection<ExpansionOption>> GetExpansionOptionsAsync()
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Select(x => new ExpansionOption(x.Code, x.Title))
                .ToList();

            return Task.FromResult<IReadOnlyCollection<ExpansionOption>>(availableExpansions);
        }

        public Task<IReadOnlyCollection<IExpansion>> GetExpansionsAsync()
        {
            var availableExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .ToList();

            return Task.FromResult<IReadOnlyCollection<IExpansion>>(availableExpansions);
        }
    }
}
