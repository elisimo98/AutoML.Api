using AutoML.Domain.Models.Contracts;
using MediatR;

namespace AutoML.Application.Models.Commands
{
    public record TrainModelConfigCommand(string TenantId, string Name) : IRequest<TrainingResponse>;
}
