using MediatR;
using Munchkin.Core.Extensions;
using Munchkin.Runtime.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Services
{
    public class CharityOptionsHandler : IRequestHandler<CharityOptionsQuery, CharityOptions>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;

        public CharityOptionsHandler(
            ITableRepository tableRepository,
            IPlayerRepository playerRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<CharityOptions> Handle(CharityOptionsQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);
            var player = await _playerRepository.GetPlayerByNicknameAsync(request.PlayerNickname);
            var cardsForGiveaway = player.AllCards();
            return new CharityOptions(player, cardsForGiveaway);
        }
    }
}
