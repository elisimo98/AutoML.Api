using AutoML.Data.Models;
using AutoML.Domain.Models;

namespace AutoML.Data.Mappers
{
    public static class ModelConfigMapper
    {
        public static ModelConfig ToDomain(this ModelConfigEntity entity)
        {
            return new ModelConfig()
            {
                TenantId = entity.TenantId,
                Name = entity.Name,
                Description = entity.Description,
                FileName = entity.FileName,
                TestSize = entity.TestSize,
                RandomState = entity.RandomState,
                Epochs = entity.Epochs,
                ModelType = entity.ModelType,
                TargetColumn = entity.TargetColumn,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }

}
