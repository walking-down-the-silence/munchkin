using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.Extensions.Mappers;
using Munchkin.Api.ViewModels;
using Munchkin.Services.Lobby.Services;
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
            var gameRoomVm = gameRoom.ToVM();
            return Ok(gameRoomVm);
        }

        [ProducesResponseType(typeof(GameRoomVM), StatusCodes.Status200OK)]
        [HttpGet("{gameRoomId}")]
        public async Task<IActionResult> Get(int gameRoomId)
        {
            var gameRoom = await _gameRoomService.GetGameRoom(gameRoomId);
            var gameRoomVm = gameRoom.ToVM();
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
    }
}
