using CryptoExchange.Core.Command;
using CryptoExchange.Services.Commands;
using System;
using System.Threading.Tasks;

namespace CryptoExchange.Core.Interfaces.Service
{
    public interface ICoinService
    {
        Task<CommandReturn> GetCryptoCoins();
        Task<CommandReturn> GetCryptoRates(GetCryptoRatesCommand command);
        Task<CommandReturn> GetHistorialRates(GetHistorialRatesCommand getHistorialRates);
       
    }
}
