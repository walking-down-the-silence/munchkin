using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Client.Services;
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

        [HttpGet("{gameId}/players")]
        public async Task<IActionResult> Get(int gameId)
        {
            var players = await _gameEngineService.GetPlayersAsync(gameId);
            return Ok(players);
        }

        [HttpGet("{gameId}/players/{playerId}")]
        public async Task<IActionResult> Get(int gameId, int playerId)
        {
            var players = await _gameEngineService.GetPlayerByIdAsync(gameId, playerId);
            return Ok(players);
        }

        [HttpGet("{gameId}/players/{playerId}/hand")]
        public async Task<IActionResult> CardsInHand(int gameId, int playerId)
        {
            return Ok();
        }

        [HttpGet("{gameId}/players/{playerId}/equipped")]
        public async Task<IActionResult> EquippedCards(int gameId, int playerId)
        {
            return Ok();
        }

        [HttpGet("{gameId}/players/{playerId}/backpack")]
        public async Task<IActionResult> CardsInBackpack(int gameId, int playerId)
        {
            return Ok();
        }

        [HttpPut("{gameId}/players/{playerId}/cards/{cardId}/storage")]
        public async Task<IActionResult> ChangeCardStorage(int gameId, int playerId, int cardId, [FromBody] CardStorageReferenceVM vm)
        {
            await _playerService.ChangeCardStorage(gameId, playerId, cardId, vm.StorageType);
            return Ok();
        }

        [HttpGet("{gameId}/discarded-treasures")]
        public async Task<IActionResult> GetDiscardedTreasuresCards(int gameId)
        {
            return Ok();
        }

        [HttpGet("{gameId}/discarded-doors")]
        public async Task<IActionResult> GetDiscardedDoorsCards(int gameId)
        {
            return Ok();
        }

        [HttpPut("{gameId}/treasures/{cardId}/owner")]
        public async Task<IActionResult> ChangeTreasureCardOwner(int gameId, int playerId, [FromBody] CardOwnerReferenceVM vm)
        {
            return Ok();
        }

        [HttpPut("{gameId}/doors/{cardId}/owner")]
        public async Task<IActionResult> ChangeDoorCardOwner(int gameId, int playerId, [FromBody] CardOwnerReferenceVM vm)
        {
            return Ok();
        }
    }
}
