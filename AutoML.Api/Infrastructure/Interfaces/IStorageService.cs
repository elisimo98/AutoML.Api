using AutoML.Api.Models;

namespace AutoML.Api.Infrastructure.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// Uploads a CSV file to Storage.
        /// </summary>
        /// <param name="tenantId">Tenant identifier</param>
        /// <param name="fileStream">A <see cref="Stream"/></param>
        /// <param name="fileName">Name of the file</param>
        Task UploadDatasetAsync(string tenantId, Stream fileStream, string fileName);

        /// <summary>
        /// Retrieves a CSV file from Storage.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="tenantId">Tenant identifier</param>
        /// <returns>A <see cref="Stream"/></returns>
        Task<ServiceResult<Stream>> GetDatasetAsync(string tenantId, string fileName);

        /// <summary>
        /// Retrieves a list of file names for a given tenant.
        /// </summary>
        /// <param name="tenantId">Tenant identifier</param>
        /// <returns></returns>
        Task<List<string>> GetFileNamesForTenantAsync(string tenantId);
    }
}
