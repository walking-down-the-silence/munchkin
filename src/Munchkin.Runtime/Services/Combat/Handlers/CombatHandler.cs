using MediatR;
using Munchkin.Core.Model.Phases;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Handlers
{
    public class CombatHandler : IRequestHandler<CombatQuery, Combat>
    {
        private readonly ITableRepository _tableRepository;

        public CombatHandler(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public async Task<Combat> Handle(CombatQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);
            return Combat.From(table);
        }
    }
}
