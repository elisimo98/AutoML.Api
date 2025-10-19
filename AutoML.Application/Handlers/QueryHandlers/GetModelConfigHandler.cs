using AutoML.Application.Mappers;
using AutoML.Application.Models.DTOs;
using AutoML.Application.Models.Queries;
using AutoML.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoML.Application.Handlers.QueryHandlers
{
    public class GetModelConfigHandler : IRequestHandler<GetModelConfigQuery, ModelConfigDto?>
    {
        private readonly IModelConfigRepository repository;
        private readonly ILogger<GetModelConfigHandler> logger;

        public GetModelConfigHandler(IModelConfigRepository repository, ILogger<GetModelConfigHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ModelConfigDto?> Handle(GetModelConfigQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            logger.LogDebug("Handling {Handler} for ModelConfigId: {Id}", nameof(GetModelConfigHandler), request.Id);

            var modelConfig = await repository.GetByIdAsync(request.TenantId, request.Id);

            if (modelConfig is null)
            {
                logger.LogWarning("ModelConfig with ID {Id} not found", request.Id);
                return null;
            }

            logger.LogInformation("Successfully retrieved ModelConfig with ID {Id}", request.Id);
            logger.LogTrace("ModelConfig entity: {ModelConfig}", modelConfig);

            return modelConfig?.ToDto();
        }
    }
}
