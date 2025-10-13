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
                Id = entity.Id,
                Name = entity.Name,
                TenantId = entity.TenantId
            };
        }
    }

}
