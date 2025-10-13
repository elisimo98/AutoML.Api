namespace AutoML.Domain.Models
{
    public class ModelConfig
    {
        public int Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
