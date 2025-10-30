using AutoML.Application.Mappers;
using AutoML.Application.Models.Commands;
using AutoML.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoML.Application.Handlers.CommandHandlers
{
    public class CreateModelConfigHandler : IRequestHandler<CreateModelConfigCommand>
    {
        private readonly IModelConfigRepository repository;
        private readonly ILogger<CreateModelConfigHandler> logger;

        public CreateModelConfigHandler(IModelConfigRepository repository, ILogger<CreateModelConfigHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(CreateModelConfigCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            logger.LogDebug("Handling {Handler} for creating ModelConfig: {ModelConfig}", nameof(CreateModelConfigHandler), request);

            var modelConfig = request.ToEntity();

            await repository.CreateAsync(modelConfig);

            logger.LogInformation("Successfully created ModelConfig {Id}", modelConfig.Name);
        }
    }
}
