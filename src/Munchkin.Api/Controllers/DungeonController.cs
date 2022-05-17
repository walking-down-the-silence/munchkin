using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/dungeon")]
    [ApiController]
    public class DungeonController : ControllerBase
    {
        public DungeonController()
        {

        }


        [HttpPost("{dungeonId}/kick-open-the-door")]
        public async Task<IActionResult> KickOpenTheDoor(string dungeonId)
        {
            return Ok();
        }
    }
}
