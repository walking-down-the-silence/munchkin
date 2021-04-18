using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/game-rooms")]
    [ApiController]
    public class GameRoomController : ControllerBase
    {
        // POST api/game-room
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            return Ok();
        }

        // GET api/game-room
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        // GET api/game-room/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok();
        }

        // PUT api/game-room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/game-room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}
