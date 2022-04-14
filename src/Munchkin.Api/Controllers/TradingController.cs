using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munchkin.Api.ViewModels.Trading;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Api.Controllers
{
    [Route("api/trades")]
    [ApiController]
    public class TradingController : ControllerBase
    {
        [ProducesResponseType(typeof(TradeVM), StatusCodes.Status200OK)]
        [HttpGet("tradeId")]
        public async Task<IActionResult> GetTrade(string tradeId)
        {
            var emptyCollection = Enumerable.Empty<string>().ToList();
            var result = new TradeVM(
                tradeId,
                new TradeSideVM(
                    "player_1313154",
                    emptyCollection,
                    emptyCollection),
                new TradeSideVM(
                    "player_524141",
                    emptyCollection,
                    emptyCollection));
            return Ok(result);
        }
    }
}
