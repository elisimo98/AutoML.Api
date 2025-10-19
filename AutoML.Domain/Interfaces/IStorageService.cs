using AutoML.Domain.Models;

namespace AutoML.Api.Infrastructure.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// Delete a CSV file from Storage.
        /// </summary>
        /// <param name="tenantId">Tenant identifier</param>
        /// <param name="fileName">Name of the file</param>
        /// <returns>A <see cref="ServiceResult"/></returns>
        Task<ServiceResult> DeleteDatasetAsync(string tenantId, string fileName);

        /// <summary>
        /// Uploads a CSV file to Storage.
        /// </summary>
        /// <param name="tenantId">Tenant identifier</param>
        /// <param name="fileStream">A <see cref="Stream"/></param>
        /// <param name="fileName">Name of the file</param>
        /// <returns>A <see cref="ServiceResult"/></returns>
        Task<ServiceResult> UploadDatasetAsync(string tenantId, Stream fileStream, string fileName);

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
        /// <returns>A collection of file names</returns>
        Task<ServiceResult<List<string>>> GetFileNamesForTenantAsync(string tenantId);
    }
}
