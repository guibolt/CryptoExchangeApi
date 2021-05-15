using CryptoExchange.Core.IntegrationModels;
using System;
using System.Threading.Tasks;

namespace CryptoExchange.Core.Interfaces.Service
{
    public interface ICoinService
    {
        Task<MainReturn> GetCryptoCoins();
        Task<MainReturn> GetCryptoRates(string fistCoin, string secondCoin);
        Task<MainReturn> GetHistorialRates(string coinSymbol, DateTime initialDate);
       
    }
}
