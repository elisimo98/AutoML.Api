using AutoML.Data.Models;
using AutoML.Domain.Models;

namespace AutoML.Data.Interfaces
{
    public interface IModelConfigRepository
    {
        /// <summary>
        /// Get a ModelConfig by Id
        /// </summary>
        /// <param name="id">Identifier for model config</param>
        /// <returns>A <see cref="ModelConfig"/></returns>
        Task<ModelConfig?> GetByIdAsync(long id);

        /// <summary>
        /// Add a new ModelConfig
        /// </summary>
        /// <param name="entity">A <see cref="ModelConfigEntity"/></param>
        /// <returns></returns>
        Task AddAsync(ModelConfigEntity entity);

        /// <summary>
        /// Update an existing ModelConfig
        /// </summary>
        /// <param name="entity">A <see cref="ModelConfigEntity"/></param>
        /// <returns></returns>
        Task UpdateAsync(ModelConfigEntity entity);

        /// <summary>
        /// Delete a ModelConfig by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Whether or not the operation was successful</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// Get all ModelConfigs for a given tenant
        /// </summary>
        /// <param name="tenantExternalId">External identifier for a given tenant</param>
        /// <returns>A collection of <see cref="ModelConfig"/></returns>
        Task<List<ModelConfig>> GetByTenantIdAsync(string tenantExternalId);
    }
}
