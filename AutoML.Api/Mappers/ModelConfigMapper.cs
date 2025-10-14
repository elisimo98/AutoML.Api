using AutoML.Api.Models;
using AutoML.Application.Models.DTOs;

namespace AutoML.Api.Mappers
{
    public static class ModelConfigMapper
    {
        public static ModelConfigResponse ToResponse(this ModelConfigDto dto)
        {
            return new ModelConfigResponse()
            {
                Id = dto.Id,
                Name = dto.Name,
                TenantId = dto.TenantId
            };
        }
    }

}
