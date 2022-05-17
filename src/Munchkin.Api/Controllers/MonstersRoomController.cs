using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/monsters")]
    [ApiController]
    public class MonstersRoomController : ControllerBase
    {
        [HttpPost("{monsterId}/run-away")]
        public async Task<IActionResult> RunAway(string monsterId)
        {
            return Ok();
        }

        [HttpPost("{monsterId}/take-bad-stuff")]
        public async Task<IActionResult> TakeBadStuff(string monsterId)
        {
            return Ok();
        }
    }
}
