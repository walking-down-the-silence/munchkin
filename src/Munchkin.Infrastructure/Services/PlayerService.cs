﻿using Munchkin.Core.Extensions;
using Munchkin.Runtime;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Infrastructure.Services
{
    public class PlayerService
    {
        private readonly IGameEngineRepository _gameEngineRepository;

        public PlayerService(
            IGameEngineRepository gameEngineRepository)
        {
            _gameEngineRepository = gameEngineRepository ?? throw new ArgumentNullException(nameof(gameEngineRepository));
        }

        public async Task EquipItem(int gameId, int playerId, int cardId)
        {
            var game = await _gameEngineRepository.GetGameByIdAsync(gameId);

            if (game is null)
                throw new ArgumentNullException(nameof(game));

            // TODO: find player by id instead of First()
            var player = game.Table.Players.First();

            if (player is null)
                throw new ArgumentNullException(nameof(player));

            // TODO: find player card by id instead of First()
            var card = player.AllCards().First();

            if (card is null)
                throw new ArgumentNullException(nameof(card));

            player.Equip(card);
        }

        public async Task PutInBackpack(int gameId, int playerId, int cardId)
        {
            var game = await _gameEngineRepository.GetGameByIdAsync(gameId);

            if (game is null)
                throw new ArgumentNullException(nameof(game));

            // TODO: find player by id instead of First()
            var player = game.Table.Players.First();

            if (player is null)
                throw new ArgumentNullException(nameof(player));

            // TODO: find player card by id instead of First()
            var card = player.AllCards().First();

            if (card is null)
                throw new ArgumentNullException(nameof(card));

            player.PutInBackpack(card);
        }
    }
}