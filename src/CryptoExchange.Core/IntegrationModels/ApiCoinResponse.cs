using System;
using System.Collections.Generic;

namespace CryptoExchange.Core.IntegrationModels
{
    public class ApiCoinResponse
    {
        public DateTime time { get; set; }
        public string asset_id_base { get; set; }
        public string asset_id_quote { get; set; }
        public double rate { get; set; }
        public List<ExchangeAsset> src_side_base { get; set; }
        public List<ExchangeAsset> src_side_quote { get; set; }
    }
}
