namespace AutoML.Domain.Models.Contracts
{
    public class TrainingRequest
    {
        public string FileName { get; set; } = string.Empty;
        public double TestSize { get; set; } = 0.2f;
        public int RandomState { get; set; } = 42;
        public string? ModelType { get; set; }
        public required string TargetColumn { get; set; }
    }
}
