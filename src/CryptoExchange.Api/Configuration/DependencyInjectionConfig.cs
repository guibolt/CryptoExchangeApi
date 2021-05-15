using CryptoExchange.Core.Helpers;
using CryptoExchange.Core.Helpers.Api;
using CryptoExchange.Core.Interfaces.Api;
using CryptoExchange.Core.Interfaces.Service;
using CryptoExchange.Services.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoExchange.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IApiSetup>(new ApiSetup(configuration["CoinApi:ApiUrl"],configuration["CoinApi:ApiKey"], 
                                                    configuration["CoreApiUrl"], configuration["CoinLayerApi:ApiUrl"],
                                                     configuration["CoinLayerApi:ApiKey"]));
            services.AddHttpClient();
            services.AddScoped<ICoinService, CoinService>();

            return services;
        }
    }
}
