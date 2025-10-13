using AutoML.Data.Interfaces;
using AutoML.Data.Mappers;
using AutoML.Data.Models;
using AutoML.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoML.Data.Repositories
{
    public class ModelConfigRepository : IModelConfigRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ModelConfigRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ModelConfig?> GetByIdAsync(long id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);

            var entity = await dbContext.ModelConfigs
                .AsNoTracking()
                .FirstOrDefaultAsync(mc => mc.Id == id);

            if (entity == null)
                return null;

            return ModelConfigMapper.ToDomain(entity);
        }

        /// <inheritdoc/>
        public async Task AddAsync(ModelConfigEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await dbContext.ModelConfigs.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(ModelConfigEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            dbContext.ModelConfigs.Update(entity);
            await dbContext.SaveChangesAsync();
        }


        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(long id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);

            var entity = await dbContext.ModelConfigs.FindAsync(id);
            
            if (entity == null)
                return false;

            dbContext.ModelConfigs.Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<List<ModelConfigEntity>> GetByTenantIdAsync(string tenantExternalId)
        {
            ArgumentException.ThrowIfNullOrEmpty(tenantExternalId);

            var list = await dbContext.ModelConfigs
                .AsNoTracking()
                .Where(mc => mc.Tenant.ExternalId == tenantExternalId)
                .ToListAsync();

            return list;
        }
    }
}
