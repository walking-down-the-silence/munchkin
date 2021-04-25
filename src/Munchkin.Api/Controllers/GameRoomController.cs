using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.ViewModels;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/game-rooms")]
    [ApiController]
    public class GameRoomController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameRoomCreateAsLeaderVM vm)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("{gameRoomId}")]
        public async Task<IActionResult> Get(int gameRoomId)
        {
            return Ok();
        }

        [HttpPut("{gameRoomId}")]
        public async Task<IActionResult> Put(int gameRoomId, [FromBody] string value)
        {
            return Ok();
        }

        [HttpDelete("{gameRoomId}")]
        public async Task<IActionResult> Delete(int gameRoomId)
        {
            return NoContent();
        }
    }
}
