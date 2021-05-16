using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoExchange.Api.Configuration.HeathCheck
{
    public static class HeathCheckConfig
    {
        public static IEndpointRouteBuilder MapHeathConfig(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
                ResponseWriter = WriteResponse
            });

            return endpoints;
        }

        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            var heathCheckResult = new
            {
                status = result.Status.ToString(),
                results = result.Entries.Select(entry => new
                {
                    key = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description,
                    data = entry.Value.Data.Select(data => new { data.Key, data.Value})
                }).ToList()
            };

            string json = JsonConvert.SerializeObject(heathCheckResult, Formatting.Indented, new JsonSerializerSettings());

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(json);
        }
    }
}
