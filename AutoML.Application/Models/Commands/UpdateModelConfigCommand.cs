using MediatR;

namespace AutoML.Application.Models.Commands
{
    public record UpdateModelConfigCommand(
        long Id,
        string Name,
        string Description
    ) : IRequest;
}
