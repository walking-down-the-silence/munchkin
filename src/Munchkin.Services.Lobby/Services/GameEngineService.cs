using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
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

        public async Task<IGameEngine> CreateEngine(int gameRoomId)
        {
            var gameRoom = await _gameRoomRepository.GetGameRoomByIdAsync(gameRoomId);

            if (gameRoom is null)
                throw new ArgumentNullException(nameof(gameRoom));

            var users = await gameRoom.GetUsers();
            var players = users.Select(ToPlayer).ToArray();
            var selectedExpansionOptions = await gameRoom
                .GetExpansionSelections()
                .ContinueWith(x => x.Result.Where(y => y.Selected).ToArray());
            var selectedExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Where(x => selectedExpansionOptions.Any(y => string.Equals(y.Code, x.Code)))
                .ToArray();

            // TODO: replace game engine instantiation with proper solution
            IGameEngine gameEngine = null;//new GameEngine(_mediator, selectedExpansions, players);
            gameEngine = await _gameEngineRepository.SaveGameAsync(gameEngine);

            return gameEngine;
        }

        public Task<IGameEngine> GetGameEngineAsync(int gameId)
        {
            return _gameEngineRepository.GetGameByIdAsync(gameId);
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

        private static Player ToPlayer(User user)
        {
            return new Player(user.UserName, user.IsMale ? EGender.Male : EGender.Female);
        }
    }
}
