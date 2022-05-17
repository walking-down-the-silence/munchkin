using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class CharityService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;

        public CharityService(
            ITableRepository charityRepository,
            IPlayerRepository playerRepository)
        {
            _tableRepository = charityRepository ?? throw new ArgumentNullException(nameof(charityRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public Task GiveAwayAsync(string tableId, string treasureCardId, string playerGiverNickname, string playerTakerNickname)
        {
            return ExecuteAndSave(tableId, async table =>
            {
                var playerGiver = await _playerRepository.GetPlayerByNicknameAsync(playerGiverNickname);
                var playerTaker = await _playerRepository.GetPlayerByNicknameAsync(playerTakerNickname);
                var treasureCard = await _tableRepository.GetCardByIdAsync(tableId, treasureCardId) as TreasureCard;

                var charityUpdated = Charity.GiveAway(table, playerGiver, treasureCard, playerTaker);
                return (charityUpdated, charityUpdated);
            });
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
