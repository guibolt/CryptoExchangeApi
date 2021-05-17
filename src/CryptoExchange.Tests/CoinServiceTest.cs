using CryptoExchange.Core.Command;
using CryptoExchange.Core.Interfaces.Api;
using CryptoExchange.Core.Interfaces.Service;
using CryptoExchange.Services.Classes;
using CryptoExchange.Services.Commands;
using KissLog;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CryptoExchange.Tests
{
    public class CoinServiceTest
    {
        private Mock<IApiSetup> _apiSetup = new Mock<IApiSetup>();
        private Mock<HttpClient> _client = new Mock<HttpClient>();
        private Mock<ILogger> _logger = new Mock<ILogger>();
                
        [Fact]
        public async Task Must_Return_Error_In_GetCryptoRates_When_Informing_Wrong_parameters()
        {
            //Arrange
            var command = new GetCryptoRatesCommand(It.IsAny<string>(), It.IsAny<string>());
            var service = new CoinService(_apiSetup.Object,_client.Object,_logger.Object);

            //Act
            var commandReturn = await service.GetCryptoRates(command);

            //Assert
            Assert.NotNull(commandReturn);
            Assert.False(commandReturn.Success);
        }
        [Fact]
        public async Task Must_Return_Success_In_GetCryptoRates_When_Informing_Ok_parameters()
        {
            //Arrange
            var param = new
            {
                fisrtCoin = "BTC",
                secondCoin = "ETH"
            };
           
            var command = new GetCryptoRatesCommand(param.fisrtCoin, param.secondCoin);
            var service = new CoinService(_apiSetup.Object, _client.Object, _logger.Object);

            _apiSetup.Setup(x => x.GetCoinApiKey()).Returns("62EC2F71-A84A-4F0E-8722-E1C3BB844D88");
            _apiSetup.Setup(x => x.GetCoinApiUrl()).Returns("https://rest.coinapi.io/v1/");

            //Act
            var commandReturn = await service.GetCryptoRates(command);

            //Assert
            Assert.NotNull(commandReturn);
            Assert.True(commandReturn.Success);
        }

        [Fact]
        public async Task Must_Return_Error_In_GetHistorialRates_When_Informing_Wrong_parameters()
        {
            //Arrange
            var command = new GetHistorialRatesCommand(It.IsAny<string>(), It.IsAny<DateTime>());
            var service = new CoinService(_apiSetup.Object, _client.Object, _logger.Object);

            //Act
            var commandReturn = await service.GetHistorialRates(command);

            //Assert
            Assert.NotNull(commandReturn);
            Assert.False(commandReturn.Success);
        }


        [Fact]
        public async Task Must_Return_Success_In_GetHistorialRates_When_Informing_Ok_parameters()
        {
            //Arrange
            var param = new
            {
                fisrtCoin = "BTC",
                initialDate = DateTime.Now
            };

            var command = new GetHistorialRatesCommand(param.fisrtCoin, param.initialDate);
            var service = new CoinService(_apiSetup.Object, _client.Object, _logger.Object);

            _apiSetup.Setup(x => x.GetCoinLoreApiUrl()).Returns("http://api.coinlayer.com/");
            _apiSetup.Setup(x => x.GetCoinLoreKey()).Returns("db185d73e2bc10752d0ea3c98a9565fc");

            //Act
            var commandReturn = await service.GetHistorialRates(command);

            //Assert
            Assert.NotNull(commandReturn);
            Assert.True(commandReturn.Success);
        }
    }
}
