namespace AutoML.Domain.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// Uploads a CSV file to Storage.
        /// </summary>
        /// <param name="fileStream">A <see cref="Stream"/></param>
        /// <param name="fileName">Name of the file</param>
        Task UploadCsvAsync(Stream fileStream, string fileName);

        /// <summary>
        /// Retrieves a CSV file from Storage.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <returns>A <see cref="Stream"/></returns>
        Task<Stream> GetCsvAsync(string fileName);
    }
}
