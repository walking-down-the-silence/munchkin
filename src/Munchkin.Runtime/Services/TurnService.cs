using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class TurnService
    {
        private readonly ITurnRepository _turnRepository;
        private readonly ITableRepository _tableRepository;

        public TurnService(
            ITurnRepository turnRepository,
            ITableRepository tableRepository)
        {
            _turnRepository = turnRepository ?? throw new ArgumentNullException(nameof(turnRepository));
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public Task<Turn> GetTurnAsync(string tableId) => _turnRepository.GetTurnByTableIdAsync(tableId);

        public async Task<Turn> CreateAsync(string tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            return await _turnRepository.SaveTurnAsync(Turn.From(table));
        }

        public async Task<Turn> NextAsync(string tableId)
        {
            var turn = await _turnRepository.GetTurnByTableIdAsync(tableId);
            return await _turnRepository.SaveTurnAsync(Turn.Next(turn));
        }
    }
}
