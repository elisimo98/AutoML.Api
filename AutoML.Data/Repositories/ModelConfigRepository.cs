using AutoML.Data.Interfaces;
using AutoML.Data.Mappers;
using AutoML.Data.Models;
using AutoML.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoML.Data.Repositories
{
    /// <inheritdoc/>
    public class ModelConfigRepository : IModelConfigRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ModelConfigRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ModelConfig?> GetByIdAsync(string tenantId, long id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            ArgumentException.ThrowIfNullOrEmpty(tenantId);

            var entity = await dbContext.ModelConfigs
                .AsNoTracking()
                .Where(mc => mc.TenantId == tenantId && mc.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
                return null;

            return ModelConfigMapper.ToDomain(entity);
        }

        public async Task<long> AddAsync(ModelConfigEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await dbContext.ModelConfigs.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(ModelConfigEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            dbContext.ModelConfigs.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(string tenantId, long id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            ArgumentException.ThrowIfNullOrEmpty(tenantId);

            var entity = await dbContext.ModelConfigs
                .Where(mc => mc.TenantId == tenantId && mc.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
                return false;

            dbContext.ModelConfigs.Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ModelConfig>> GetByTenantIdAsync(string tenantId)
        {
            ArgumentException.ThrowIfNullOrEmpty(tenantId);

            var list = await dbContext.ModelConfigs
                .AsNoTracking()
                .Where(mc => mc.TenantId == tenantId)
                .Select(mc => ModelConfigMapper.ToDomain(mc))
                .ToListAsync();

            return list;
        }
    }
}
