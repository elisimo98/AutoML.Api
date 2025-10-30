namespace AutoML.Domain.Models
{
    public class ModelConfig
    {
        public int Id { get; set; }
        public string TenantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public double TestSize { get; set; } = 0.2f;
        public int RandomState { get; set; } = 42;
        public int Epochs { get; set; } = 50;
        public string ModelType { get; set; } = string.Empty;
        public string TargetColumn { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string? ToString()
        {
            return base.ToString() + $"{nameof(Id)}: {Id}, " +
                $"{nameof(TenantId)}: {TenantId}, " +
                $"{nameof(Name)}: {Name}, " +
                $"{nameof(Description)}: {Description}, " +
                $"{nameof(FileName)}: {FileName}, " +
                $"{nameof(TestSize)}: {TestSize}, " +
                $"{nameof(RandomState)}: {RandomState}, " +
                $"{nameof(Epochs)}: {Epochs}, " +
                $"{nameof(ModelType)}: {ModelType}, " +
                $"{nameof(TargetColumn)}: {TargetColumn}, " +
                $"{nameof(UpdatedAt)}: {UpdatedAt}, " +
                $"{nameof(CreatedAt)}: {CreatedAt}";
        }
    }
}
