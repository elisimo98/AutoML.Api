using AutoML.Api.Infrastructure.Interfaces;
using AutoML.Domain.Interfaces;
using AutoML.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoML.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiBaseUrl = configuration["ApiSettings:ApiBaseUrl"]
                ?? throw new InvalidOperationException("ApiSettings:ApiBaseUrl is missing in configuration.");

            // Register client
            services.AddHttpClient<ITrainingClient, TrainingClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Register services
            services.AddScoped<ITrainingClient, TrainingClient>();
            services.AddScoped<IStorageService, BlobStorageService>();

            return services;
        }
    }
}
