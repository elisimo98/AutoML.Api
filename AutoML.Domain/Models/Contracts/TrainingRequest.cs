namespace AutoML.Domain.Models.Contracts
{
    public class TrainingRequest
    {
        public string FileName { get; set; } = string.Empty;
        public double TestSplitRatio { get; set; } = 0.2f;
        public List<List<float>> Features { get; set; } = new();
        public List<float> Labels { get; set; } = new();
        public string? SpecificModelType { get; set; }
    }
}
