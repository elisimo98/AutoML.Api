using AutoML.Application.Models.Commands;
using AutoML.Application.Models.DTOs;
using AutoML.Data.Models;
using AutoML.Domain.Models;

namespace AutoML.Application.Mappers
{
    public static class ModelConfigMapper
    {
        public static ModelConfigDto ToDto(this ModelConfig domain)
        {
            return new ModelConfigDto()
            {
                Id = domain.Id,
                TenantId = domain.TenantId,
                FileName = domain.FileName,
                TestSize = domain.TestSize,
                RandomState = domain.RandomState,
                Epochs = domain.Epochs,
                ModelType = domain.ModelType,
                TargetColumn = domain.TargetColumn
            };
        }

        public static ModelConfigEntity ToEntity(this CreateModelConfigCommand request)
        {
            return new ModelConfigEntity()
            {
                TenantId = request.TenantId,
                FileName = request.FileName,
                TestSize = request.TestSize,
                RandomState = request.RandomState,
                Epochs = request.Epochs,
                ModelType = request.ModelType,
                TargetColumn = request.TargetColumn
            };
        }
    }
}
