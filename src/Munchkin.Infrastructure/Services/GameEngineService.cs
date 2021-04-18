using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Runtime;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Runtime.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Infrastructure.Services
{
    public class GameEngineService
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _expansionProvider;
        private readonly IGameEngineRepository _gameEngineRepository;
        private readonly IGameRoomRepository _gameRoomRepository;

        public GameEngineService(
            IMediator mediator,
            IServiceProvider expansionRegister,
            IGameEngineRepository gameEngineRepository,
            IGameRoomRepository gameRoomRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _expansionProvider = expansionRegister ?? throw new ArgumentNullException(nameof(expansionRegister));
            _gameEngineRepository = gameEngineRepository ?? throw new ArgumentNullException(nameof(gameEngineRepository));
            _gameRoomRepository = gameRoomRepository ?? throw new ArgumentNullException(nameof(gameRoomRepository));
        }

        public async Task<GameEngine> CreateEngine(int gameRoomId)
        {
            var gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(gameRoomId);

            if (gameRoom is null)
                throw new ArgumentNullException(nameof(gameRoom));

            var players = gameRoom.Players.Select(ToPlayer).ToArray();
            var selectedExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Where(x => gameRoom.SelectedExpansions.Any(y => string.Equals(y.Code, x.Code)))
                .ToArray();
            var gameEngine = new GameEngine(_mediator, selectedExpansions, players);

            gameEngine = await _gameEngineRepository.SaveGameAsync(gameEngine);

            return gameEngine;
        }

        public async Task<IReadOnlyCollection<Player>> GetPlayersAsync(int gameId)
        {
            var game = await _gameEngineRepository.GetGameByIdAsync(gameId);

            if (game is null)
                throw new ArgumentNullException(nameof(game));

            return game.Table.Players.ToArray();
        }

        public async Task<Player> GetPlayerByIdAsync(int gameId, int playerId)
        {
            var game = await _gameEngineRepository.GetGameByIdAsync(gameId);

            if (game is null)
                throw new ArgumentNullException(nameof(game));

            // TODO: find player by id instead of First()
            return game.Table.Players.First();
        }

        private static Player ToPlayer(User p)
        {
            return new Player(p.UserName, p.IsMale ? EGender.Male : EGender.Female);
        }
    }
}
