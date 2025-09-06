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
        public async Task UploadCsvAsync(Stream fileStream, string fileName)
        {
            ArgumentNullException.ThrowIfNull(fileStream);
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        /// <inheritdoc/>
        public async Task<Stream> GetCsvAsync(string fileName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException($"Blob '{fileName}' not found in container '{containerName}'.");
            }

            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;

            return stream;
        }

    }
}
