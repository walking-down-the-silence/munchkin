using MediatR;
using Munchkin.Core.Model.Phases;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Handlers
{
    public class CombatStatsHandler : IRequestHandler<CombatStatsQuery, CombatStats>
    {
        private readonly ITableRepository _tableRepository;

        public CombatStatsHandler(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public async Task<CombatStats> Handle(CombatStatsQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);
            return CombatStats.From(table);
        }
    }
}
