using AutoML.Application.Models.DTOs;
using MediatR;

namespace AutoML.Application.Models.Queries
{
    public record GetModelConfigQuery(string TenantId, string Name) : IRequest<ModelConfigDto>;
}
