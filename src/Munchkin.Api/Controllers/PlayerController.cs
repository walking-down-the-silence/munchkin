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
            var user = await _playerService.CreatePlayerAsync(vm.Nickname, vm.IsMale);
            var userVm = user.ToVM();
            return Ok(userVm);
        }

        [ProducesResponseType(typeof(PlayerVM), StatusCodes.Status200OK)]
        [HttpGet("{nickname}")]
        public async Task<IActionResult> Get(string nickname)
        {
            var user = await _playerService.GetUserByNicknameAsync(nickname);
            var userVm = user.ToVM();
            return Ok(userVm);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{nickname}/hand")]
        public async Task<IActionResult> CardsInHand(string tableId, string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var cardVms = player.YourHand.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{nickname}/equipped")]
        public async Task<IActionResult> EquippedCards(string tableId, string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var cardVms = player.Equipped.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{nickname}/backpack")]
        public async Task<IActionResult> CardsInBackpack(string tableId, string nickname)
        {
            var player = await _playerService.GetUserByNicknameAsync(nickname);
            var cardVms = player.Backpack.Select(x => x.ToVM()).ToArray();
            return Ok(cardVms);
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/players/{nickname}/cards/{cardId}/storage")]
        public async Task<IActionResult> ChangeCardStorage(string tableId, string nickname, int cardId, [FromBody] CardStorageReferenceVM vm)
        {
            await _playerService.ChangeCardStorage(tableId, nickname, cardId, vm.StorageType);
            return Ok();
        }
    }
}
