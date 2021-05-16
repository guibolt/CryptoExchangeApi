using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CryptoExchange.Api.Configuration;
using CryptoExchange.Api.Configuration.HeathCheck;
using CryptoExchange.Api.Configuration.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using KissLog.AspNetCore;

namespace CryptoExchange.Api
{
    public class Startup
    {
        private readonly string _enableCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ResolveDependencies(Configuration);

            services.AddHealthChecks()
                .AddCheck<MemoryHealthCheck>("memory_check", HealthStatus.Unhealthy);
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoExchange.Api", Version = "v1" }));
            services.AddLogging(logging => logging.AddKissLog());

            services.AddCors(opt => opt.AddPolicy(_enableCors, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoExchange.Api v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(_enableCors);
            app.UseAuthorization();
            app.UseKissLog(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHeathConfig();
            });
        }
    }
}
