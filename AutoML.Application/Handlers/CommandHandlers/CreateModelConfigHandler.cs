using AutoML.Application.Mappers;
using AutoML.Application.Models.Commands;
using AutoML.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoML.Application.Handlers.CommandHandlers
{
    public class CreateModelConfigHandler : IRequestHandler<CreateModelConfigCommand, long>
    {
        private readonly IModelConfigRepository repository;
        private readonly ILogger<CreateModelConfigHandler> logger;

        public CreateModelConfigHandler(IModelConfigRepository repository, ILogger<CreateModelConfigHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<long> Handle(CreateModelConfigCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            logger.LogDebug("Handling {Handler} for creating ModelConfig: {ModelConfig}", nameof(CreateModelConfigHandler), request);

            var modelConfig = request.ToEntity();

            var newId = await repository.AddAsync(modelConfig);

            logger.LogInformation("Successfully created ModelConfig {Id}", newId);

            return newId;
        }
    }
}
