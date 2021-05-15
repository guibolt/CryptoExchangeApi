using CryptoExchange.Core.Interfaces.Api;

namespace CryptoExchange.Core.Helpers.Api
{
    public class ApiSetup : IApiSetup
    {
        private readonly string _coinApiUrl;
        private readonly string _apiCoinKey;
        private readonly string _apiCoreUrl;
        private readonly string _apiCoinLoreUrl;
        private readonly string _apiCoinLoreKey;

        public ApiSetup(string coinApiUrl, string apiCoinKey, string apiCoreUrl, string apiCoinLoreUrl, string apiCoinLoreKey)
        {
            _coinApiUrl = coinApiUrl;
            _apiCoinKey = apiCoinKey;
            _apiCoreUrl = apiCoreUrl;
            _apiCoinLoreUrl = apiCoinLoreUrl;
            _apiCoinLoreKey = apiCoinLoreKey;
        }

        public string GetCoinApiUrl() => _coinApiUrl;
        public string GetCoinApiKey() => _apiCoinKey;
        public string GetCoreApiUrl() => _apiCoreUrl;
        public string GetCoinLoreApiUrl() => _apiCoinLoreUrl;
        public string GetCoinLoreKey() => _apiCoinLoreKey;
    }
}
