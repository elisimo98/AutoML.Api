using AutoML.Api.Infrastructure.Interfaces;
using AutoML.Domain.Interfaces;
using AutoML.Web.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoML.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiBaseUrl = configuration["MLWorkerApiBaseUrl"]
                ?? throw new InvalidOperationException("MLWorkerApiBaseUrl is missing in configuration.");

            // Register client
            services.AddHttpClient<ITrainingClient, TrainingClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Register BlobServiceClient using the connection string from config
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connectionString = config["AzureBlobStorage:ConnectionString"];
                return new BlobServiceClient(connectionString);
            });

            // Register services
            services.AddScoped<ITrainingClient, TrainingClient>();
            services.AddScoped<IStorageService, BlobStorageService>();

            return services;
        }
    }
}
