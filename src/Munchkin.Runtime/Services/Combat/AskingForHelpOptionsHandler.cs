using MediatR;
using Munchkin.Core.Extensions;
using Munchkin.Runtime.Abstractions;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Phases
{
    public class AskingForHelpOptionsHandler : IRequestHandler<AskingForHelpOptionsQuery, AskingForHelp>
    {
        private readonly ITableRepository _tableRepository;

        public AskingForHelpOptionsHandler(
            ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public async Task<AskingForHelp> Handle(AskingForHelpOptionsQuery request, CancellationToken cancellationToken)
        {
            // Gets the state for asking players for help process based on all players at the table.
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);
            var askedPlayers = table.ActionLog
                .OfType<AskingForHelpPlayerEvent>()
                .ToArray();

            // TODO: filter by plaer selected above
            var playersToAsk = ImmutableList.CreateRange(table.Players
                .Where(player => player != table.Turns.Current.Player)
                .Where(player => !player.IsDead()));

            // TODO: decide if this object requires the player recently asked
            // or simplly all players left to ask
            return new AskingForHelp(playersToAsk, default);
        }
    }
}
