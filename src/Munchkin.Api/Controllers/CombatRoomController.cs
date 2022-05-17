using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class CombatRoomController : ControllerBase
    {
        [HttpPost("{combatRoomId}/reward")]
        public async Task<IActionResult> Reward(string combatRoomId)
        {
            return Ok();
        }

        [HttpPost("{combatRoomId}/run-away")]
        public async Task<IActionResult> RunAway(string combatRoomId)
        {
            return Ok();
        }
    }
}
