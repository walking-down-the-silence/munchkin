using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Runtime.Abstractions.GameEngines;
using Munchkin.Runtime.Abstractions.UserAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class PlayerService
    {
        private readonly IGameEngineRepository _gameEngineRepository;
        private readonly IPlayerRepository _userRepository;

        public PlayerService(
            IGameEngineRepository gameEngineRepository,
            IPlayerRepository clusterClient)
        {
            _gameEngineRepository = gameEngineRepository ?? throw new ArgumentNullException(nameof(gameEngineRepository));
            _userRepository = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
        }

        public async Task<Player> CreatePlayerAsync(string nickname, bool isMale)
        {
            var gender = isMale ? EGender.Male : EGender.Female;
            var player = new Player(nickname, gender);
            await _userRepository.SavePlayerAsync(player);
            return player;
        }

        public Task<Player> GetUserByNicknameAsync(string nickname)
        {
            return _userRepository.GetPlayerByNicknameAsync(nickname);
        }

        public async Task ChangeCardStorage(string tableId, string playerId, int cardId, string storageType)
        {
            var game = await _gameEngineRepository.GetGameByIdAsync(tableId);

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

            switch (storageType.Trim().ToLower())
            {
                case StorageTypes.PlayerBackpack:
                    player.PutInBackpack(card);
                    break;
                case StorageTypes.PlayerHand:
                    player.Equip(card);
                    break;
                case StorageTypes.GameTable:
                    break;
            }
        }

        public static class StorageTypes
        {
            public const string PlayerBackpack = "player.backpack";
            public const string PlayerHand = "player.hand";
            public const string GameTable = "game.table";
        }
    }
}
