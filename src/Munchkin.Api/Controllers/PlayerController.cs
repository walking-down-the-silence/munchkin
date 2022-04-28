using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.Extensions.Mappers;
using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerController(
            PlayerService playerService)
        {
            _playerService = playerService ?? throw new System.ArgumentNullException(nameof(playerService));
        }

        [ProducesResponseType(typeof(PlayerVM), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlayerCreateVM vm)
        {
            var player = await _playerService.CreatePlayerAsync(vm.Nickname, vm.IsMale);
            var playerVm = player.ToVM();
            return Ok(playerVm);
        }

        [ProducesResponseType(typeof(PlayerVM), StatusCodes.Status200OK)]
        [HttpGet("{nickname}")]
        public async Task<IActionResult> Get(string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var playerVm = player.ToVM();
            return Ok(playerVm);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{nickname}/hand")]
        public async Task<IActionResult> CardsInHand(string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var playerHandVms = player.YourHand.Select(x => x.ToVM()).ToArray();
            return Ok(playerHandVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{nickname}/equipped")]
        public async Task<IActionResult> EquippedCards(string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var playerEquippedVms = player.Equipped.Select(x => x.ToVM()).ToArray();
            return Ok(playerEquippedVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{nickname}/backpack")]
        public async Task<IActionResult> CardsInBackpack(string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var playerBackpackVms = player.Backpack.Select(x => x.ToVM()).ToArray();
            return Ok(playerBackpackVms);
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{nickname}/cards/{cardId}/storage")]
        public async Task<IActionResult> ChangeCardStorage(string nickname, int cardId, [FromBody] CardStorageReferenceVM vm)
        {
            await _playerService.ChangeCardStorage(nickname, cardId, vm.StorageType);
            return Ok();
        }
    }
}
