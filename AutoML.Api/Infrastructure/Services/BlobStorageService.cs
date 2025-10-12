using AutoML.Api.Infrastructure.Interfaces;
using AutoML.Api.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AutoML.Web.Services
{
    public class BlobStorageService : IStorageService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly ILogger<BlobStorageService> logger;
        private readonly string containerName;

        public BlobStorageService(IConfiguration configuration, BlobServiceClient blobServiceClient, ILogger<BlobStorageService> logger)
        {
            this.blobServiceClient = blobServiceClient;
            this.logger = logger;
            var containerName = configuration["AzureBlobStorage:ContainerName"];

            if (string.IsNullOrEmpty(containerName))
            {
                throw new InvalidOperationException("Azure Blob Storage container name is missing or empty.");
            }

            this.containerName = containerName!;
        }

        /// <inheritdoc/>
        public async Task UploadDatasetAsync(string tenantId, Stream fileStream, string fileName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            var blobName = $"tenant/{tenantId}/dataset/{fileName}";

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<Stream>> GetDatasetAsync(string tenantId, string fileName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(tenantId);
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);

            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                var blobName = $"tenant/{tenantId}/dataset/{fileName}";
                var blobClient = containerClient.GetBlobClient(blobName);

                var exists = await blobClient.ExistsAsync();
                if (!exists)
                {
                    return ServiceResult<Stream>.Failure($"Dataset '{fileName}' not found for tenant '{tenantId}'.");
                }

                var stream = new MemoryStream();
                await blobClient.DownloadToAsync(stream);
                stream.Position = 0;

                return ServiceResult<Stream>.Success(stream);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving dataset {FileName} for tenant {TenantId}", fileName, tenantId);
                return ServiceResult<Stream>.Failure("An error occurred while retrieving the dataset.");
            }
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetFileNamesForTenantAsync(string tenantId)
        {
            ArgumentNullException.ThrowIfNull(tenantId);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var prefix = $"tenant/{tenantId}/dataset/";
            var fileNames = new List<string>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(prefix: prefix))
            {
                string fullName = blobItem.Name;
                string fileName = fullName.Substring(prefix.Length);

                fileNames.Add(fileName);
            }

            return fileNames;
        }
    }
}
