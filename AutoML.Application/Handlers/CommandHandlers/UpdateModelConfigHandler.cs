using AutoML.Application.Models.Commands;
using MediatR;

namespace AutoML.Application.Handlers.CommandHandlers
{
    public class UpdateModelConfigHandler : IRequestHandler<UpdateModelConfigCommand>
    {
        public Task Handle(UpdateModelConfigCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
