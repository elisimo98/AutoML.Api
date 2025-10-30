using AutoML.Data.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using AutoML.Data.Models;
using AutoML.Domain.Models;
using AutoML.Data.Mappers;

namespace AutoML.Data.Repositories
{
    public class ModelConfigRepository : IModelConfigRepository
    {
        private readonly IMongoCollection<ModelConfigEntity> collection;

        public ModelConfigRepository(IMongoDatabase database, IOptions<MongoDbSettings> settings)
        {
            collection = database.GetCollection<ModelConfigEntity>(settings.Value.CollectionName);
        }

        public async Task<List<ModelConfig>> GetAllAsync()
        {
            var results = await collection.FindAsync(_ => true);
            var modelConfigs = await results.ToListAsync();

            if (modelConfigs.Count <= 0)
            {
                return new List<ModelConfig>();
            }

            return modelConfigs.ConvertAll(c => c.ToDomain());
        }

        public async Task<ModelConfig?> GetAsync(string id)
        {
            var result = await collection.FindAsync(c => c.Id == id);
            var modelConfig = await result.FirstOrDefaultAsync();
            return modelConfig?.ToDomain();
        }

        public async Task<string> CreateAsync(ModelConfigEntity config)
        {
            await collection.InsertOneAsync(config);
            return config.Name;
        }

        public async Task UpdateAsync(ModelConfigEntity config)
            => await collection.ReplaceOneAsync(c => c.Id == config.Id, config);

        public async Task DeleteAsync(string id)
            => await collection.DeleteOneAsync(c => c.Id == id);
    }
}