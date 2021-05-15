using System.Collections.Generic;

namespace CryptoExchange.Core.IntegrationModels
{
    public class ApiCoinLoreResponse
    {
        public List<Coin> data { get; set; }
        public ApiInfo info { get; set; }
    }
}
