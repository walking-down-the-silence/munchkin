using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.Extensions.Mappers;
using Munchkin.Api.ViewModels;
using Munchkin.Services.Lobby.Services;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
        }

        [ProducesResponseType(typeof(UserVM), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateVM vm)
        {
            var user = await _userService.CreateUserAsync(vm.UserId, vm.Username, vm.IsMale);
            var userVm = user.ToVM();
            return Ok(userVm);
        }

        [ProducesResponseType(typeof(UserVM), StatusCodes.Status200OK)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var userVm = user.ToVM();
            return Ok(userVm);
        }
    }
}
