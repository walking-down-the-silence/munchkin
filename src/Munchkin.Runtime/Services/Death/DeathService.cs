using MediatR;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Queries;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class DeathService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMediator _mediator;

        public DeathService(
            ITableRepository tableRepository,
            IPlayerRepository playerRepository,
            IMediator mediator)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task LootTheBodyAsync(string tableId, string giverNickname, string takerNickname, string cardId)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var result = await _mediator.Send(new LootTheBodyOptionsQuery(tableId, giverNickname));

                var giver = await _playerRepository.GetPlayerByNicknameAsync(giverNickname);
                var taker = await _playerRepository.GetPlayerByNicknameAsync(takerNickname);
                var card = await _tableRepository.GetCardByIdAsync(tableId, cardId);
                
                var tableUpdated = Death.LootTheBody(table, giver, taker, result.TakenBy, card);
                return (tableUpdated, tableUpdated);
            });
        }

        private async Task<(Table Table, TResult Result)> ExecuteAndSave<TResult>(
            string tableId,
            Func<Table, Task<(Table Table, TResult Result)>> action)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            var result = await action(table);
            var tableUpdated = await _tableRepository.SaveTableAsync(result.Table);
            return (tableUpdated, result.Result);
        }
    }
}
