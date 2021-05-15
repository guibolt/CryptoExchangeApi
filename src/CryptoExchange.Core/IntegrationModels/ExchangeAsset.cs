using System;

namespace CryptoExchange.Core.IntegrationModels
{
    public class ExchangeAsset
    {
        public DateTime time { get; set; }
        public string asset { get; set; }
        public double rate { get; set; }
        public double volume { get; set; }
        public string spotlight { get; set; }
    }
}
