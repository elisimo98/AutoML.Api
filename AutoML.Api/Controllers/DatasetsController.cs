using AutoML.Api.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoML.Api.Controllers
{
    [ApiController]
    [Route("api/tenants/{tenantId}/datasets")]
    public class DatasetsController : ControllerBase
    {
        private readonly IStorageService storageService;
        private readonly ILogger<DatasetsController> logger;

        public DatasetsController(IStorageService storageService, ILogger<DatasetsController> logger)
        {
            this.storageService = storageService;
            this.logger = logger;
        }

        [HttpGet("{datasetId}")]
        public async Task<IActionResult> GetDataset(string tenantId, string datasetId)
        {
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(datasetId))
                return BadRequest("Tenant Id and Dataset Id cannot be empty");

            try
            {
                var result = await storageService.GetDatasetAsync(tenantId, datasetId);

                if (!result.IsSuccess)
                    return NotFound(result.Errors);

                logger.LogInformation("Dataset {DatasetId} successfully retrieved for tenant {TenantId}", datasetId, tenantId);

                return File(result.Data!, "application/octet-stream", datasetId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error retrieving dataset {DatasetId} for tenant {TenantId}", datasetId, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
