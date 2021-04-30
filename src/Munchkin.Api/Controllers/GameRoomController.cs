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
    [Route("api/game-rooms")]
    [ApiController]
    public class GameRoomController : ControllerBase
    {
        private readonly GameRoomService _gameRoomService;

        public GameRoomController(GameRoomService gameRoomService)
        {
            _gameRoomService = gameRoomService ?? throw new System.ArgumentNullException(nameof(gameRoomService));
        }

        [ProducesResponseType(typeof(GameRoomVM), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameRoomCreateAsLeaderVM vm)
        {
            var gameRoom = await _gameRoomService.CreateRoomAsLeader(vm.UserId);
            var gameRoomVm = await gameRoom.ToVM();
            return Ok(gameRoomVm);
        }

        [ProducesResponseType(typeof(GameRoomVM), StatusCodes.Status200OK)]
        [HttpGet("{gameRoomId}")]
        public async Task<IActionResult> Get(int gameRoomId)
        {
            var gameRoom = await _gameRoomService.GetGameRoom(gameRoomId);
            var gameRoomVm = await gameRoom.ToVM();
            return Ok(gameRoomVm);
        }

        [ProducesResponseType(typeof(GameRoomJoinRoomResultVM), StatusCodes.Status200OK)]
        [HttpPut("{gameRoomId}/players/{playerId}")]
        public async Task<IActionResult> Put(int gameRoomId, int playerId)
        {
            var joinResponse = await _gameRoomService.JoinTheRoom(gameRoomId, playerId);
            return Ok(joinResponse);
        }

        [ProducesResponseType(typeof(GameRoomJoinRoomResultVM), StatusCodes.Status200OK)]
        [HttpDelete("{gameRoomId}/players/{playerId}")]
        public async Task<IActionResult> Delete(int gameRoomId, int playerId)
        {
            var joinResponse = await _gameRoomService.LeaveTheRoom(gameRoomId, playerId);
            return Ok(joinResponse);
        }

        [ProducesResponseType(typeof(ICollection<GameRoomExpansionSelectionVM>), StatusCodes.Status200OK)]
        [HttpGet("{gameRoomId}/expansions")]
        public async Task<IActionResult> GetAvailableExpansions(int gameRoomId)
        {
            var expansionSelections = await _gameRoomService.GetExpansionSelections(gameRoomId);
            var expansionSelectionsVms = expansionSelections.Select(x => x.ToVM()).ToArray();
            return Ok(expansionSelectionsVms);
        }

        [ProducesResponseType(typeof(GameRoomExpansionSelectionVM), StatusCodes.Status200OK)]
        [HttpPut("{gameRoomId}/expansions/{code}/selected")]
        public async Task<IActionResult> SelectExapansion(int gameRoomId, string code)
        {
            var selectionResponse = await _gameRoomService.MarkExpansionSelection(gameRoomId, code, true);
            var selectionResponseVm = selectionResponse;
            return Ok(selectionResponse);
        }
    }
}
