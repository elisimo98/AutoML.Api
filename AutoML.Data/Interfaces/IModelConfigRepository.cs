using AutoML.Data.Models;
using AutoML.Domain.Models;

namespace AutoML.Data.Interfaces
{
    public interface IModelConfigRepository
    {
        Task<List<ModelConfig>> GetAllAsync();
        Task<ModelConfig?> GetAsync(string id);
        Task<string> CreateAsync(ModelConfigEntity config);
        Task UpdateAsync(ModelConfigEntity config);
        Task DeleteAsync(string id);
    }
}
