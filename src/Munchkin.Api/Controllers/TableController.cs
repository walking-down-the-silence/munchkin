using Microsoft.AspNetCore.Mvc;
using Munchkin.Infrastructure.Services;
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

        [HttpPut("{gameId}/players/{playerId}/equipped/{cardId}")]
        public async Task<IActionResult> EquipItem(int gameId, int playerId, int cardId)
        {
            await _playerService.EquipItem(gameId, playerId, cardId);
            return Ok();
        }

        [HttpGet("{gameId}/players/{playerId}/backpack")]
        public async Task<IActionResult> CardsInBackpack(int gameId, int playerId)
        {
            return Ok();
        }

        [HttpPut("{gameId}/players/{playerId}/backpack/{cardId}")]
        public async Task<IActionResult> PutInBackpack(int gameId, int playerId, int cardId)
        {
            await _playerService.PutInBackpack(gameId, playerId, cardId);
            return Ok();
        }
    }
}
