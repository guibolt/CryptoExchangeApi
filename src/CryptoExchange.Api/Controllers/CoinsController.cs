using CryptoExchange.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CryptoExchange.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinService _coinService;

        public CoinsController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpGet("Rates")]
        public async Task<IActionResult> ReturnRates([FromQuery] string firstCoin, [FromQuery] string secondCoin)
        {
            var mainReturn = await _coinService.GetCryptoRates(firstCoin, secondCoin);
            return mainReturn.Success ? Ok(mainReturn) : BadRequest(mainReturn);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnCoins()
        {
            var mainReturn = await _coinService.GetCryptoCoins();
            return mainReturn.Success ? Ok(mainReturn) : BadRequest(mainReturn);
        }

        [HttpGet("Rates/historical")]
        public async Task<IActionResult> ReturnHistoricalRates([FromQuery] string coinSymbol,[FromQuery] DateTime initialDate)
        {
            var mainReturn = await _coinService.GetHistorialRates(coinSymbol, initialDate);
            return mainReturn.Success ? Ok(mainReturn) : BadRequest(mainReturn);
        }
    }
}
