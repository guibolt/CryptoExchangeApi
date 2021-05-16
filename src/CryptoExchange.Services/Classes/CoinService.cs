using CryptoExchange.Core.Interfaces.Api;
using CryptoExchange.Core.Interfaces.Service;
using CryptoExchange.Core.IntegrationModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using KissLog;
using CryptoExchange.Services.Commands;
using CryptoExchange.Core.Command;

namespace CryptoExchange.Services.Classes
{
    public class CoinService : BaseService, ICoinService
    {
        private readonly IApiSetup _apiUrl;
        private readonly HttpClient _client;

        public CoinService(IApiSetup apiUrl, HttpClient client, ILogger logger) : base(logger)
        {
            _apiUrl = apiUrl;
            _client = client;
        }

        public async Task<CommandReturn> GetCryptoCoins()
        {
            string methodName = nameof(GetCryptoCoins);

            try
            {
                var response = await _client.GetAsync($"{_apiUrl.GetCoreApiUrl()}tickers/?start=0&limit=50");
                var jsonContent = await response.Content.ReadAsStringAsync();

                var responseApiObj = JsonConvert.DeserializeObject<ApiCoinLoreResponse>(jsonContent);
                var lstCoints = responseApiObj.data.Select(x => new
                {
                    x.id,
                    x.name,
                    x.symbol
                });

                _logger.Info(lstCoints, methodName);
                return new CommandReturn(true,"Consulta realizada com sucesso!", lstCoints);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, methodName);
                return new CommandReturn(false,"Erro na consulta, favor tentar novamente");
            }
        }

        public async Task<CommandReturn> GetCryptoRates(GetCryptoRatesCommand command )
        {
            string methodName = nameof(GetCryptoRates);

            if (!command.IsValid())
            {
                var errors = command.Errors();
                _logger.Error(errors, methodName);

                return new CommandReturn(false, errors, "");
            }

         
            try
            {
                _client.DefaultRequestHeaders.Add("X-CoinAPI-Key", _apiUrl.GetCoinApiKey());
                var response = await _client.GetAsync($"{_apiUrl.GetCoinApiUrl()}exchangerate/{command.FirstCoin}/{command.SecondCoin}");
                var jsonContent = await response.Content.ReadAsStringAsync();

                var responseApiObj = JsonConvert.DeserializeObject<ApiCoinResponse>(jsonContent);

                var exchanges = responseApiObj.src_side_base.OrderBy(x => x.rate).ToList();

                for (int i = 0; i < 3; i++)
                {
                    exchanges[i].spotlight = "purple";
                }

                var resultObj = new
                {
                    requestDate = DateTime.Now.ToString(""),
                    coinRate = responseApiObj.rate,
                    exchanges
                };


                _logger.Info(resultObj, methodName);
                return new CommandReturn(true,"Consulta realizada com sucesso!", resultObj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, methodName);
                return new CommandReturn(false,"Erro na consulta, favor tentar novamente");
            }
        }

        public async Task<CommandReturn> GetHistorialRates(GetHistorialRatesCommand command)
        {
            string methodName = nameof(GetHistorialRates);

            if (!command.IsValid())
            {
                var errors = command.Errors();
                _logger.Error(errors, methodName);

                return new CommandReturn(false, errors, "");
            }
            
            try
            {
                var objList = new List<object>();
                var actualDate = DateTime.Now;
                var monthsQuantity = ((actualDate.Year - command.InitialDate.Year) * 12) + actualDate.Month - command.InitialDate.Month;

                await AddCoins(command.CoinSymbol, command.InitialDate, objList, monthsQuantity);

                CoinLoreResponse responseApiObj = await GetCoinRecord(command.CoinSymbol, actualDate);
                AddCoin(command.CoinSymbol, objList, responseApiObj);


                _logger.Info(objList, methodName);
                return new CommandReturn(true,"Consulta realizada com sucesso!", objList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, methodName);
                return new CommandReturn(false,"Erro na consulta, favor tentar novamente");
            }

        }

        private async Task AddCoins(string coinSymbol, DateTime initialDate, List<object> objList, int monthsQuantity)
        {
            for (int i = 0; i < monthsQuantity; i++)
            {
                CoinLoreResponse responseApiObj = await GetCoinRecord(coinSymbol, initialDate);
                AddCoin(coinSymbol, objList, responseApiObj);

                initialDate = initialDate.AddMonths(1);
            }
        }

        private static void AddCoin(string coinSymbol, List<object> objList, CoinLoreResponse responseApiObj)
        {
            var jObj = (JObject)responseApiObj.rates;
            var coinValue = jObj.Value<string>(coinSymbol);

            var resultObj = new
            {
                responseApiObj.date,
                coinValue,
                coinSearched = coinSymbol
            };

            objList.Add(resultObj);
        }

        private async Task<CoinLoreResponse> GetCoinRecord(string coinSymbol, DateTime initialDate)
        {
            var response = await _client.GetAsync($"{_apiUrl.GetCoinLoreApiUrl()}{initialDate.ToString("yyyy-MM-dd")}?access_key={_apiUrl.GetCoinLoreKey()}&symbols={coinSymbol}");
            var jsonContent = await response.Content.ReadAsStringAsync();

            var responseApiObj = JsonConvert.DeserializeObject<CoinLoreResponse>(jsonContent);
            return responseApiObj;
        }
    }
}
