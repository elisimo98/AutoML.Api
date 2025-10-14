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

        // GET: api/tenants/{tenantId}/datasets/{datasetId}
        [HttpGet("{datasetId}")]
        public async Task<IActionResult> GetDataset([FromRoute] string tenantId, [FromRoute] string datasetId)
        {
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(datasetId))
                return BadRequest("Tenant Id and Dataset Id cannot be empty");

            var result = await storageService.GetDatasetAsync(tenantId, datasetId);

            if (!result.IsSuccess)
                return NotFound(result.Errors);

            logger.LogInformation("Dataset {DatasetId} successfully retrieved for tenant {TenantId}", datasetId, tenantId);

            return File(result.Data!, "application/octet-stream", datasetId);
        }

        // GET: api/tenants/{tenantId}/datasets
        [HttpGet]
        public async Task<IActionResult> GetAllDatasetNames([FromRoute] string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
                return BadRequest("Tenant Id cannot be empty");

            try
            {
                var result = await storageService.GetFileNamesForTenantAsync(tenantId);

                if (!result.IsSuccess)
                    return NotFound(result.Errors);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error listing datasets for tenant {TenantId}", tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // POST: api/tenants/{tenantId}/datasets
        [HttpPost]
        public async Task<IActionResult> UploadDataset([FromRoute] string tenantId, [FromForm] IFormFile file)
        {
            if (string.IsNullOrEmpty(tenantId) || file == null || file.Length == 0)
                return BadRequest("Tenant Id and file cannot be empty");

            try
            {
                using var stream = file.OpenReadStream();
                await storageService.UploadDatasetAsync(tenantId, stream, file.FileName);

                logger.LogInformation("Dataset {FileName} successfully uploaded for tenant {TenantId}", file.FileName, tenantId);
                return Ok(new { Message = "Dataset uploaded successfully" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error uploading dataset {FileName} for tenant {TenantId}", file.FileName, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // DELETE: api/tenants/{tenantId}/datasets/{fileName}
        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteDataset([FromRoute] string tenantId, [FromRoute] string fileName)
        {
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(fileName))
                return BadRequest("Tenant Id and file name cannot be empty");

            try
            {
                await storageService.DeleteDatasetAsync(tenantId, fileName);

                logger.LogInformation("Dataset {FileName} successfully deleted for tenant {TenantId}", fileName, tenantId);
                return Ok(new { Message = "Dataset deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error deleting dataset {FileName} for tenant {TenantId}", fileName, tenantId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
