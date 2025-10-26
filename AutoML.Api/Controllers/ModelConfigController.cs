using AutoML.Api.Mappers;
using AutoML.Api.Models;
using AutoML.Application.Models.Commands;
using AutoML.Application.Models.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoML.Api.Controllers
{
    [ApiController]
    [Route("api/tenants/{tenantId}/modelconfig")]
    public class ModelConfigController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidator<CreateModelConfigRequest> createValidator;
        private readonly ILogger<ModelConfigController> logger;

        public ModelConfigController(
            IMediator mediator, 
            IValidator<CreateModelConfigRequest> createValidator, 
            ILogger<ModelConfigController> logger)
        {
            this.mediator = mediator;
            this.createValidator = createValidator;
            this.logger = logger;
        }

        // GET: api/tenants/{tenantId}/modelconfig/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelConfig([FromRoute] string tenantId, [FromRoute] long id)
        {
            logger.LogInformation("Received request for tenantId={TenantId}, id={Id}", tenantId, id);

            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("Tenant Id cannot be empty");

            if (id < 0)
                return BadRequest("Model Config Id cannot be less than 0");

            try
            {
                var result = await mediator.Send(new GetModelConfigQuery(tenantId, id));

                if (result is null)
                    return NotFound();

                logger.LogInformation("Model Config {Id} successfully retrieved for tenant {TenantId}", id, tenantId);

                return Ok(result.ToResponse());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error fetching ModelConfig {Id} for tenant {TenantId}", id, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // GET: api/tenants/{tenantId}/modelconfig
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllModelConfig([FromRoute] string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("Tenant Id cannot be empty");

            try
            {
                var result = await mediator.Send(new ListModelConfigQuery(tenantId));

                if (result is null)
                    return NotFound();

                logger.LogInformation("ModelConfigs successfully retrieved for tenant {TenantId}", tenantId);

                var mappedResults = result.ConvertAll(r => r.ToResponse());

                return Ok(mappedResults);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error listing ModelConfigs for tenant {TenantId}", tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // POST: api/tenants/{tenantId}/modelconfig
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateModelConfig([FromRoute] string tenantId, [FromBody] CreateModelConfigRequest request)
        {
            if (string.IsNullOrEmpty(tenantId) || request is null)
                return BadRequest("Tenant Id and request body cannot be empty");

            try
            {
                var validationResult = await createValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    logger.LogWarning("Validation failed for Upsert Model Config request: {Errors}", validationResult.Errors);
                    return BadRequest(validationResult.Errors);
                }

                await mediator.Send(new CreateModelConfigCommand(
                    tenantId,
                    request.FileName,
                    request.TestSize,
                    request.RandomState,
                    request.Epochs,
                    request.ModelType,
                    request.TargetColumn));

                logger.LogInformation("ModelConfig successfully created for tenant {TenantId}", tenantId);
                return Ok(new { Message = "ModelConfig created successfully" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error uploading ModelConfig for tenant {TenantId}", tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // DELETE: api/tenants/{tenantId}/modelconfig/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelConfig([FromRoute] string tenantId, [FromRoute] long id)
        {
            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("Tenant Id cannot be empty");

            if (id < 0)
                return BadRequest("Model Config Id cannot be less than 0");

            try
            {
                await mediator.Send(new DeleteModelConfigCommand(tenantId, id));

                logger.LogInformation("ModelConfig {Id} successfully deleted for tenant {TenantId}", id, tenantId);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error deleting ModelConfig {Id} for tenant {TenantId}", id, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // POST: api/tenants/{tenantId}/modelconfig/{id}/train
        [Authorize]
        [HttpPost("train")]
        public async Task<IActionResult> Train([FromRoute] string tenantId, [FromRoute] long id)
        {
            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("Tenant Id cannot be empty");

            if (id < 0)
                return BadRequest("Model Config Id cannot be less than 0");

            try
            {
                await mediator.Send(new TrainModelConfigCommand(tenantId, id));

                logger.LogInformation("Model Config {Id} successfully trained for tenant {TenantId}", id, tenantId);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error training ModelConfig {Id} for tenant {TenantId}", id, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}
