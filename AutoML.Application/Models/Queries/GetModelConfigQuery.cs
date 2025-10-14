using AutoML.Application.Models.DTOs;
using MediatR;

namespace AutoML.Application.Models.Queries
{
    public record GetModelConfigQuery(long Id) : IRequest<ModelConfigDto>;
}
