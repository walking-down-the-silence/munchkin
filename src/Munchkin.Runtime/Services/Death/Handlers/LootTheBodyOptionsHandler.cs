using MediatR;
using Munchkin.Core.Extensions;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Queries;
using Munchkin.Runtime.Services;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Handlers
{
    public class LootTheBodyOptionsHandler : IRequestHandler<LootTheBodyOptionsQuery, LootTheBodyOptions>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPlayerRepository _playerRepository;

        public LootTheBodyOptionsHandler(
            ITableRepository tableRepository,
            IPlayerRepository playerRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }
        public async Task<LootTheBodyOptions> Handle(LootTheBodyOptionsQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableByIdAsync(request.TableId);
            var player = await _playerRepository.GetPlayerByNicknameAsync(request.PlayerNickname);
            var cardsForGiveaway = player.AllCards();

            // NOTE: Starting with the player with the highest Level, everyone else chooses one
            // card... in case of ties in Level, roll a die.
            // Dead characters cannot receive cards for any reason, not even Charity, and
            // cannot level up or win the game.
            var otherPlayers = ImmutableArray.CreateRange(table.Players
                .Where(p => p != player)
                .Where(p => !p.IsDead())
                .OrderByDescending(p => p.Level));

            // NOTE: Looting The Body: Lay out your hand beside the cards you had in play
            // (making sure not to include the cards mentioned above). If you have an Item
            // carried by a Hireling or attached to a Cheat!card, separate those cards.
            var cardsToLoot = ImmutableArray.CreateRange(player.Die());

            // TODO: Ensure that player avatar is dead when reaching this point
            return new LootTheBodyOptions(player, otherPlayers, cardsForGiveaway);
        }
    }
}
