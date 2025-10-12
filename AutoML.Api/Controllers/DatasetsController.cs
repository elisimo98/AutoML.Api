using AutoML.Domain.Interfaces;
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

        [HttpGet]
        [HttpGet("{datasetId}")]
        public async Task<IActionResult> GetDataset(string tenantId, string datasetId)
        {
            try
            {
                var dataset = await this.storageService.GetDatasetAsync(tenantId, datasetId);
                return Ok(dataset);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error retrieving dataset {DatasetId} for tenant {TenantId}", datasetId, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
