using MediatR;

namespace AutoML.Application.Models.Commands
{
    public record DeleteModelConfigCommand(string TenantId, long Id) : IRequest;
}