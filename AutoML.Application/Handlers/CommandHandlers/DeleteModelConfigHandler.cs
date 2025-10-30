using AutoML.Application.Models.Commands;
using AutoML.Data.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoML.Application.Handlers.CommandHandlers
{
    public class DeleteModelConfigHandler : IRequestHandler<DeleteModelConfigCommand>
    {
        private readonly IModelConfigRepository repository;
        private readonly ILogger<DeleteModelConfigHandler> logger;

        public DeleteModelConfigHandler(IModelConfigRepository repository, ILogger<DeleteModelConfigHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(DeleteModelConfigCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            logger.LogDebug("Handling {Handler} for creating ModelConfig: {ModelConfig}", nameof(DeleteModelConfigHandler), request);

            await repository.DeleteAsync(request.id);

            logger.LogInformation("Successfully deleted ModelConfig {Id}", request.id);
        }
    }
}
