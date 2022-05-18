using MediatR;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.RunningAway.Queries;
using Munchkin.Runtime.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.RunningAway.Handlers
{
    public class RunningAwayPlayerHandler : IRequestHandler<RunningAwayPlayerQuery, RunningAwayPlayer>
    {
        private readonly ITableRepository _tableRepository;

        public RunningAwayPlayerHandler(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public async Task<RunningAwayPlayer> Handle(RunningAwayPlayerQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);
            return RunningAwayPlayer.From(table);
        }
    }
}
