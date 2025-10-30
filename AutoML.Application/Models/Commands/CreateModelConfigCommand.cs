using MediatR;

namespace AutoML.Application.Models.Commands
{
    public record CreateModelConfigCommand(
        string TenantId,
        string Name,
        string Description,
        string FileName,
        double TestSize,
        int RandomState,
        int Epochs,
        string ModelType,
        string TargetColumn
    ) : IRequest<long>;
}
