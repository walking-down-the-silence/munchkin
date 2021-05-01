using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.Extensions.Mappers;
using Munchkin.Api.ViewModels;
using Munchkin.Services.Lobby.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly GameEngineService _gameEngineService;
        private readonly PlayerService _playerService;

        public TableController(
            GameEngineService gameEngineService,
            PlayerService playerService)
        {
            _gameEngineService = gameEngineService ?? throw new System.ArgumentNullException(nameof(gameEngineService));
            _playerService = playerService ?? throw new System.ArgumentNullException(nameof(playerService));
        }

        [ProducesResponseType(typeof(ICollection<PlayerVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/players")]
        public async Task<IActionResult> Get(int gameId)
        {
            var players = await _gameEngineService.GetPlayersAsync(gameId);
            var playerVms = players.Select(x => x.ToVM()).ToArray();
            return Ok(playerVms);
        }

        [ProducesResponseType(typeof(PlayerVM), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/players/{playerId}")]
        public async Task<IActionResult> Get(int gameId, int playerId)
        {
            var player = await _gameEngineService.GetPlayerByIdAsync(gameId, playerId);
            var playerVm = player.ToVM();
            return Ok(playerVm);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/players/{playerId}/hand")]
        public async Task<IActionResult> CardsInHand(int gameId, int playerId)
        {
            var player = await _gameEngineService.GetPlayerByIdAsync(gameId, playerId);
            var cardVms = player.YourHand.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/players/{playerId}/equipped")]
        public async Task<IActionResult> EquippedCards(int gameId, int playerId)
        {
            var player = await _gameEngineService.GetPlayerByIdAsync(gameId, playerId);
            var cardVms = player.Equipped.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/players/{playerId}/backpack")]
        public async Task<IActionResult> CardsInBackpack(int gameId, int playerId)
        {
            var player = await _gameEngineService.GetPlayerByIdAsync(gameId, playerId);
            var cardVms = player.Backpack.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{gameId}/players/{playerId}/cards/{cardId}/storage")]
        public async Task<IActionResult> ChangeCardStorage(int gameId, int playerId, int cardId, [FromBody] CardStorageReferenceVM vm)
        {
            await _playerService.ChangeCardStorage(gameId, playerId, cardId, vm.StorageType);
            return Ok();
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/discarded-treasures")]
        public async Task<IActionResult> GetDiscardedTreasuresCards(int gameId)
        {
            var gameEngine = await _gameEngineService.GetGameEngineAsync(gameId);
            var cardVms = gameEngine.Table.DiscardedTreasureCards.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameId}/discarded-doors")]
        public async Task<IActionResult> GetDiscardedDoorsCards(int gameId)
        {
            var gameEngine = await _gameEngineService.GetGameEngineAsync(gameId);
            var cardVms = gameEngine.Table.DiscardedDoorsCards.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{gameId}/treasures/{cardId}/owner")]
        public async Task<IActionResult> ChangeTreasureCardOwner(int gameId, int cardId, [FromBody] CardOwnerReferenceVM vm)
        {
            return Ok();
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{gameId}/doors/{cardId}/owner")]
        public async Task<IActionResult> ChangeDoorCardOwner(int gameId, int cardId, [FromBody] CardOwnerReferenceVM vm)
        {
            return Ok();
        }
    }
}
