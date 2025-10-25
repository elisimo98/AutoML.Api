using System.ComponentModel.DataAnnotations.Schema;

namespace AutoML.Data.Models
{
    [Table("ModelConfig")]
    public class ModelConfigEntity
    {
        public int Id { get; set; }
        public string TenantId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public double TestSize { get; set; } = 0.2f;
        public int RandomState { get; set; } = 42;
        public int Epochs { get; set; } = 50;
        public string ModelType { get; set; } = string.Empty;
        public string TargetColumn { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
