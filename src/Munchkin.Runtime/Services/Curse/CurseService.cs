using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Extensions.Threading;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class CurseService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;

        public CurseService(
            ITableRepository charityRepository,
            IPlayerRepository playerRepository)
        {
            _tableRepository = charityRepository ?? throw new ArgumentNullException(nameof(charityRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public Task<Table> ResolveAsync(string tableId, string cardId)
        {
            return ExecuteAndSave(tableId, table =>
            {
                var wishingRing = table.FindCard(x => x.Code == cardId);
                var tableUpdated = Cursing.Resolve(table, wishingRing);
                return (tableUpdated, tableUpdated).Unit();
            })
            .SelectMany(x => x.Table.Unit());
        }

        public Task<Table> TakeBadStuffAsync(string tableId, string curseCardId)
        {
            return ExecuteAndSave(tableId, table =>
            {
                var curse = table.FindCard(x => x.Code == curseCardId) as CurseCard;
                var tableUpdated = Cursing.TakeBadStuff(table, curse);
                return (tableUpdated, tableUpdated).Unit();
            })
            .SelectMany(x => x.Table.Unit());
        }

        private async Task<(Table Table, TResult Result)> ExecuteAndSave<TResult>(
            string tableId,
            Func<Table, Task<(Table Table, TResult Result)>> action)
        {
            var charity = await _tableRepository.GetTableByIdAsync(tableId);
            var result = await action(charity);
            var charityUpdated = await _tableRepository.SaveTableAsync(result.Table);
            return (charityUpdated, result.Result);
        }
    }
}
