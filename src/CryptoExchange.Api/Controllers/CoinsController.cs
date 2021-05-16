using CryptoExchange.Core.Interfaces.Service;
using CryptoExchange.Services.Commands;
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
            var command = new GetCryptoRatesCommand(firstCoin, secondCoin);
            var commandReturn = await _coinService.GetCryptoRates(command);

            return commandReturn.Success ? Ok(commandReturn) : BadRequest(commandReturn);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnCoins()
        {
            var commandReturn = await _coinService.GetCryptoCoins();
            return commandReturn.Success ? Ok(commandReturn) : BadRequest(commandReturn);
        }

        [HttpGet("Rates/historical")]
        public async Task<IActionResult> ReturnHistoricalRates([FromQuery] string coinSymbol,[FromQuery] DateTime initialDate)
        {
            var command = new GetHistorialRatesCommand(coinSymbol, initialDate);
            var commandReturn = await _coinService.GetHistorialRates(command);

            return commandReturn.Success ? Ok(commandReturn) : BadRequest(commandReturn);
        }
    }
}
