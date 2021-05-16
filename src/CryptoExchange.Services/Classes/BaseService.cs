using KissLog;

namespace CryptoExchange.Services.Classes
{
    public abstract class BaseService
    {
        protected readonly ILogger _logger;

        protected BaseService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
