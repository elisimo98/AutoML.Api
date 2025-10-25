using AutoML.Data.Models;
using AutoML.Domain.Models;

namespace AutoML.Data.Interfaces
{
    public interface IModelConfigRepository
    {
        Task<List<ModelConfig>> GetAllAsync();
        Task<ModelConfig?> GetAsync(long id);
        Task<long> CreateAsync(ModelConfigEntity config);
        Task UpdateAsync(long id, ModelConfigEntity config);
        Task DeleteAsync(long id);
    }
}
