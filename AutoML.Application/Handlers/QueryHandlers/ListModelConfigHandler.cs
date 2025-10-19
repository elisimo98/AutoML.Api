using AutoML.Application.Mappers;
using AutoML.Application.Models.DTOs;
using AutoML.Application.Models.Queries;
using AutoML.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoML.Application.Handlers.QueryHandlers
{
    public class ListModelConfigHandler : IRequestHandler<ListModelConfigQuery, List<ModelConfigDto>?>
    {
        private readonly IModelConfigRepository repository;
        private readonly ILogger<ListModelConfigHandler> logger;

        public ListModelConfigHandler(IModelConfigRepository repository, ILogger<ListModelConfigHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<List<ModelConfigDto>?> Handle(ListModelConfigQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            logger.LogDebug("Handling {Handler} for TenantId: {Id}", nameof(ListModelConfigHandler), request.TenantId);

            var modelConfigs = await repository.GetByTenantIdAsync(request.TenantId);

            if (modelConfigs is null)
            {
                logger.LogWarning("ModelConfigs for Tenant with ID {Id} not found", request.TenantId);
                return null;
            }

            logger.LogInformation("Successfully retrieved ModelConfigs for Tenant with ID {Id}", request.TenantId);
            logger.LogTrace("ModelConfig entities: {ModelConfigs}", modelConfigs);

            var mappedResults = modelConfigs.ConvertAll(r => r.ToDto());
            return mappedResults;
        }
    }
}
