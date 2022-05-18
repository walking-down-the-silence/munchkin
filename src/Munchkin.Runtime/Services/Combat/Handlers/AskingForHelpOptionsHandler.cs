using MediatR;
using Munchkin.Core.Model.Phases;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Handlers
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
            return AskingForHelp.From(table);
        }
    }
}
