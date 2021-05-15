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

namespace CryptoExchange.Services.Classes
{
    public class CoinService : ICoinService
    {
        private IApiSetup _apiUrl;
        private HttpClient _client;

        public CoinService(IApiSetup apiUrl, HttpClient client)
        {
            _apiUrl = apiUrl;
            _client = client;
        }

        public async Task<MainReturn> GetCryptoCoins()
        {
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

                return new MainReturn(" realizada com sucesso!", lstCoints);

            }
            catch (Exception)
            {
                return new MainReturn("Erro na consulta, favor tentar novamente");
            }
        }

        public async Task<MainReturn> GetCryptoRates(string first, string secondCoin)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(secondCoin))
                return new MainReturn("Erro na consulta, favor tentar novamente");

            try
            {
                _client.DefaultRequestHeaders.Add("X-CoinAPI-Key", _apiUrl.GetCoinApiKey());
                var response = await _client.GetAsync($"{_apiUrl.GetCoinApiUrl()}exchangerate/{first}/{secondCoin}");
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

                return new MainReturn("Consulta realizada com sucesso!", resultObj);
            }
            catch (Exception ex)
            {

                return new MainReturn("Erro na consulta, favor tentar novamente");
            }
        }

        public async Task<MainReturn> GetHistorialRates(string coinSymbol, DateTime initialDate)
        {
            var objList = new List<object>();
            var actualDate = DateTime.Now;
            var monthsQuantity = ((actualDate.Year - initialDate.Year) * 12) + actualDate.Month - initialDate.Month;

            await AddCoins(coinSymbol, initialDate, objList, monthsQuantity);

            CoinLoreResponse responseApiObj = await GetCoinRecord(coinSymbol, actualDate);
            AddCoin(coinSymbol, objList,responseApiObj);

            return new MainReturn("Consulta realizada com sucesso!", objList);
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
