using AutoML.Domain.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AutoML.Web.Services
{
    public class BlobStorageService : IStorageService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly string containerName;

        public BlobStorageService(IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
            var containerName = configuration["AzureBlobStorage:ContainerName"];

            if (string.IsNullOrEmpty(containerName))
            {
                throw new InvalidOperationException("Azure Blob Storage container name is missing or empty.");
            }

            this.containerName = containerName!;
        }

        /// <inheritdoc/>
        public async Task UploadCsvAsync(long tenantId, Stream fileStream, string fileName)
        {
            ArgumentNullException.ThrowIfNull(fileStream);
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);
            ArgumentNullException.ThrowIfNull(tenantId);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            var blobName = $"tenant/{tenantId}/dataset/{fileName}";

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        /// <inheritdoc/>
        public async Task<Stream> GetCsvAsync(long tenantId, string fileName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);
            ArgumentNullException.ThrowIfNull(tenantId);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobName = $"tenant/{tenantId}/dataset/{fileName}";

            var blobClient = containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException($"Blob '{fileName}' not found in container '{containerName}'.");
            }

            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;

            return stream;
        }

        public async Task<List<string>> GetFileNamesForTenantAsync(long tenantId)
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
