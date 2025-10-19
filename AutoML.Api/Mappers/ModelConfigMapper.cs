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
                FileName = dto.FileName,
                TestSize = dto.TestSize,
                RandomState = dto.RandomState,
                Epochs = dto.Epochs,
                ModelType = dto.ModelType,
                TargetColumn = dto.TargetColumn
            };
        }
    }

}
