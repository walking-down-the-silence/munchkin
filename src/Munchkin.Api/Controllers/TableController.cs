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
            var table = await _tableService.CreateAsync();
            var tableVm = table.ToVM();
            return Ok(tableVm);
        }

        [ProducesResponseType(typeof(TableVM), StatusCodes.Status200OK)]
        [HttpGet("{tableId}")]
        public async Task<IActionResult> Get(string tableId)
        {
            var table = await _tableService.GetAsync(tableId);
            var tableVm = table.ToVM();
            return Ok(tableVm);
        }

        [ProducesResponseType(typeof(ICollection<PlayerVM>), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/players")]
        public async Task<IActionResult> GetPlayers(string tableId)
        {
            var table = await _tableService.GetAsync(tableId);
            var playerVms = table.Players.Select(x => x.ToVM()).ToArray();
            return Ok(playerVms);
        }

        [ProducesResponseType(typeof(PlayerVM), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/players/{nickname}")]
        public async Task<IActionResult> Get(string tableId, string nickname)
        {
            var table = await _tableService.GetAsync(tableId);
            var player = table.Players.SingleOrDefault(x => x.Nickname == nickname);
            var playerVm = player.ToVM();
            return Ok(playerVm);
        }

        [ProducesResponseType(typeof(TableJoinResultVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/players/{nickname}")]
        public async Task<IActionResult> Put(string tableId, string nickname)
        {
            var joinResponse = await _tableService.JoinTableAsync(tableId, nickname);
            return Ok(joinResponse);
        }

        [ProducesResponseType(typeof(TableJoinResultVM), StatusCodes.Status200OK)]
        [HttpDelete("{tableId}/players/{nickname}")]
        public async Task<IActionResult> Delete(string tableId, string nickname)
        {
            var joinResponse = await _tableService.LeaveTableAsync(tableId, nickname);
            return Ok(joinResponse);
        }

        [ProducesResponseType(typeof(ICollection<TableExpansionSelectionVM>), StatusCodes.Status200OK)]
        [HttpGet("{tableId}/expansions")]
        public async Task<IActionResult> GetAvailableExpansions(string tableId)
        {
            var table = await _tableService.GetAsync(tableId);
            var expansionSelectionsVms = table.IncludedExpansions.Select(x => x.ToVM()).ToArray();
            return Ok(expansionSelectionsVms);
        }

        [ProducesResponseType(typeof(TableExpansionSelectionVM), StatusCodes.Status200OK)]
        [HttpPut("{tableId}/expansions/{code}/selected")]
        public async Task<IActionResult> SelectExapansion(string tableId, string code)
        {
            var selectionResponse = await _tableService.MarkExpansionSelectionAsync(tableId, code, true);
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
