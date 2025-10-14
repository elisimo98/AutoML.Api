namespace AutoML.Api.Models
{
    public class ModelConfigResponse
    {
        public int Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; } = null!;
    }
}
