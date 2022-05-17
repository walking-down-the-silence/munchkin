using MediatR;
using Munchkin.Core.Model.Phases;
using Munchkin.Runtime.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class RunningAwayPlayerStateHandler : IRequestHandler<RunningAwayPlayerStateQuery, RunningAwayPlayerState>
    {
        private readonly ITableRepository _tableRepository;

        public RunningAwayPlayerStateHandler(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public async Task<RunningAwayPlayerState> Handle(RunningAwayPlayerStateQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);

            var lastDiceRoll = table.ActionLog
                .OfType<RunningAwayFromMonsterDiceRollEvent>()
                .Where(x => string.Equals(x.PlayerNickname, request.PlayerNickname, StringComparison.OrdinalIgnoreCase))
                .LastOrDefault();

            return new RunningAwayPlayerState(
                lastDiceRoll.PlayerNickname,
                lastDiceRoll.DiceRollResult);
        }
    }
}
