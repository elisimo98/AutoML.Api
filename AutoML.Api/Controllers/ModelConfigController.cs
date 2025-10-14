using AutoML.Api.Mappers;
using AutoML.Application.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoML.Api.Controllers
{
    [ApiController]
    [Route("api/tenants/{tenantId}/modelconfig")]
    public class ModelConfigController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ModelConfigController> logger;

        public ModelConfigController(IMediator mediator, ILogger<ModelConfigController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        // GET: api/tenants/{tenantId}/modelconfig/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelConfig([FromRoute] string tenantId, [FromRoute] long id)
        {
            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("Tenant Id cannot be empty");

            if (id < 0)
                return BadRequest("Model Config Id cannot be less than 0");

            var result = await mediator.Send(new GetModelConfigQuery(id));

            if (result is null)
                return NotFound();

            logger.LogInformation("Model Config {Id} successfully retrieved for tenant {TenantId}", id, tenantId);

            return Ok(result.ToResponse());
        }

    }
}
