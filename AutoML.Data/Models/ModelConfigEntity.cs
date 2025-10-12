using System.ComponentModel.DataAnnotations.Schema;

namespace AutoML.Data.Models
{
    [Table("ModelConfig")]
    public class ModelConfigEntity
    {
        public int Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ConfigJson { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TenantEntity Tenant { get; set; } = null!;
    }
}
