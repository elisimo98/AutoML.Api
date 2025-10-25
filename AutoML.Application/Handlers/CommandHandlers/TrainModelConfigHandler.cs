using AutoML.Application.Models.Commands;
using AutoML.Data.Interfaces;
using AutoML.Domain.Interfaces;
using AutoML.Domain.Models.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoML.Application.Handlers.CommandHandlers
{
    public class TrainModelConfigHandler : IRequestHandler<TrainModelConfigCommand, TrainingResponse>
    {
        private readonly IModelConfigRepository repository;
        private readonly ITrainingClient trainingClient;
        private readonly ILogger<TrainModelConfigHandler> logger;

        public TrainModelConfigHandler(IModelConfigRepository repository, ITrainingClient trainingClient, ILogger<TrainModelConfigHandler> logger)
        {
            this.repository = repository;
            this.trainingClient = trainingClient;
            this.logger = logger;
        }

        public async Task<TrainingResponse> Handle(TrainModelConfigCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            logger.LogDebug("Handling {Handler} for creating ModelConfig: {ModelConfig}", nameof(TrainModelConfigHandler), request);

            var modelConfig = await repository.GetAsync(request.Id);
            if (modelConfig is null)
            {
                throw new KeyNotFoundException($"ModelConfig with ID {request.Id} not found.");
            }

            var trainingResponse = await trainingClient.TrainModelAsync(
                modelConfig.FileName, 
                modelConfig.TestSize, 
                modelConfig.TargetColumn, 
                modelConfig.ModelType);

            logger.LogInformation("Successfully trained ModelConfig {Id} for Tenant {TenantId}", request.Id, request.TenantId);

            return trainingResponse;
        }
    }
}
