namespace CryptoExchange.Core.IntegrationModels
{
    public class CoinLoreResponse
    {
        public bool success { get; set; }
        public string terms { get; set; }
        public string privacy { get; set; }
        public int timestamp { get; set; }
        public string target { get; set; }
        public bool historical { get; set; }
        public string date { get; set; }
        public dynamic rates { get; set; }
    }
}
