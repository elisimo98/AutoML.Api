using AutoML.Application.Models.DTOs;
using AutoML.Application.Models.Queries;
using MediatR;

namespace AutoML.Application.Handlers.QueryHandlers
{
    public class GetModelConfigHandler : IRequestHandler<GetModelConfigQuery, ModelConfigDto>
    {
        public async Task<ModelConfigDto> Handle(GetModelConfigQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
