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
    [Route("api/tables")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly TableService _tableService;

        public TableController(TableService gameRoomService)
        {
            _tableService = gameRoomService ?? throw new System.ArgumentNullException(nameof(gameRoomService));
        }

        [ProducesResponseType(typeof(TableVM), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TableCreateAsLeaderVM vm)
        {
            var gameRoom = await _tableService.CreateTableAsLeader(vm.LeaderNickname);
            var gameRoomVm = await gameRoom.ToVM();
            return Ok(gameRoomVm);
        }

        [ProducesResponseType(typeof(TableVM), StatusCodes.Status200OK)]
        [HttpGet("{tableId}")]
        public async Task<IActionResult> Get(string tableId)
        {
            var gameRoom = await _tableService.GetTable(tableId);
            var gameRoomVm = await gameRoom.ToVM();
            return Ok(gameRoomVm);
        }

        [ProducesResponseType(typeof(ICollection<PlayerVM>), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/players")]
        public async Task<IActionResult> GetPlayers(string tableId)
        {
            var players = await _tableService.GetPlayersAsync(tableId);
            var playerVms = players.Select(x => x.ToVM()).ToArray();
            return Ok(playerVms);
        }

        [ProducesResponseType(typeof(PlayerVM), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/players/{playerId}")]
        public async Task<IActionResult> Get(string tableId, string playerId)
        {
            var player = await _tableService.GetPlayerByIdAsync(tableId, playerId);
            var playerVm = player.ToVM();
            return Ok(playerVm);
        }

        [ProducesResponseType(typeof(TableJoinResultVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/players/{playerId}")]
        public async Task<IActionResult> Put(string tableId, string playerId)
        {
            var joinResponse = await _tableService.JoinTable(tableId, playerId);
            return Ok(joinResponse);
        }

        [ProducesResponseType(typeof(TableJoinResultVM), StatusCodes.Status200OK)]
        [HttpDelete("{tableId}/players/{playerId}")]
        public async Task<IActionResult> Delete(string tableId, string playerId)
        {
            var joinResponse = await _tableService.LeaveTable(tableId, playerId);
            return Ok(joinResponse);
        }

        [ProducesResponseType(typeof(ICollection<TableExpansionSelectionVM>), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/expansions")]
        public async Task<IActionResult> GetAvailableExpansions(string tableId)
        {
            var expansionSelections = await _tableService.GetExpansionSelections(tableId);
            var expansionSelectionsVms = expansionSelections.Select(x => x.ToVM()).ToArray();
            return Ok(expansionSelectionsVms);
        }

        [ProducesResponseType(typeof(TableExpansionSelectionVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/expansions/{code}/selected")]
        public async Task<IActionResult> SelectExapansion(string tableId, string code)
        {
            var selectionResponse = await _tableService.MarkExpansionSelection(tableId, code, true);
            var selectionResponseVm = selectionResponse;
            return Ok(selectionResponseVm);
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/discarded-treasures")]
        public async Task<IActionResult> GetDiscardedTreasuresCards(string tableId)
        {
            //var table = await _tableService.GetTable(tableId);
            //var cardVms = table.DiscardedTreasureCards.Select(x => x.ToVM()).ToArray();
            //return Ok(cardVms);
            return Ok();
        }

        [ProducesResponseType(typeof(ICollection<CardVM>), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/discarded-doors")]
        public async Task<IActionResult> GetDiscardedDoorsCards(string tableId)
        {
            //var table = await _tableService.GetTable(tableId);
            //var cardVms = table.DiscardedDoorsCards.Select(x => x.ToVM()).ToArray();
            //return Ok(cardVms);
            return Ok();
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/treasures/{cardId}/owner")]
        public async Task<IActionResult> ChangeTreasureCardOwner(string tableId, int cardId, [FromBody] CardOwnerReferenceVM vm)
        {
            return Ok();
        }

        [ProducesResponseType(typeof(CardOwnerVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/doors/{cardId}/owner")]
        public async Task<IActionResult> ChangeDoorCardOwner(string tableId, int cardId, [FromBody] CardOwnerReferenceVM vm)
        {
            return Ok();
        }
    }
}
