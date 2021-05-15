namespace CryptoExchange.Core.Interfaces.Api
{
    public interface IApiSetup
    {
        string GetCoinApiUrl();
        string GetCoinApiKey();
        string GetCoreApiUrl();
        string GetCoinLoreApiUrl();
        string GetCoinLoreKey();
    }
}
