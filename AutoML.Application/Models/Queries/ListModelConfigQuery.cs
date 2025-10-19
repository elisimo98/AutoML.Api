using AutoML.Application.Models.DTOs;
using MediatR;

namespace AutoML.Application.Models.Queries
{
    public record ListModelConfigQuery(string TenantId) : IRequest<List<ModelConfigDto>>;
}
