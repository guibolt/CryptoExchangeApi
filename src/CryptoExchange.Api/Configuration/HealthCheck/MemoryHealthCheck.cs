using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoExchange.Api.Configuration.HealthCheck
{
    public class MemoryHealthCheck : IHealthCheck
    {
         private const long THRESHOLD = 1073741824;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var allocated = GC.GetTotalMemory(false);

            var data = new Dictionary<string, object>()
            {
                { "Allocated", allocated }
            };

            var hasToMuchMemory = allocated >= THRESHOLD;

            var result = hasToMuchMemory ? context.Registration.FailureStatus : HealthStatus.Healthy;

            if(hasToMuchMemory)
                return Task.FromResult(new HealthCheckResult(result, "too much memory, excinding 1GB", data: data));

            return Task.FromResult(new HealthCheckResult(result, "app is healthy", data: data));
        }
    }
}
